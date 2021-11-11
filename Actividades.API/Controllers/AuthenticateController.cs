using Actividades.Domain.Models;
using Actividades.Domain.Models.Response;
using Actividades.Persistence.IdentityAuth;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Actividades.API.Controllers
{
	[Route("api/Authenticate")]
	[AllowAnonymous]
	public class AuthenticateController : Controller
	{
		private readonly UserManager<ApplicationUser> userManager;
		private readonly RoleManager<IdentityRole> roleManager;
		private readonly IConfiguration _configuration;
		public AuthenticateController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
		{
			this.userManager = userManager;
			this.roleManager = roleManager;
			_configuration = configuration;
		}

		[HttpPost]
		[Route("register")]
		public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel model)
		{
			var userExists = await userManager.FindByNameAsync(model.Username);

			if (userExists != null)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new ResponseAuth { Status = "Error", Message = "El usuario ya existe" });
			}

			var user = new ApplicationUser
			{
				Email = model.Email,
				SecurityStamp = Guid.NewGuid().ToString(),
				UserName = model.Username
			};

			var result = await userManager.CreateAsync(user, model.Password);

			if (result.Succeeded)
			{
				return Ok(new ResponseAuth { Status = "Success", Message = "El usuario se creó correctamente" });
			}
			else
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new ResponseAuth { Status = "Error", Message = "El usuario no pudo ser creado, la información ingresada no es valida" });
			}
		}

		[HttpPost]
		[Route("login")]
		public async Task<IActionResult> LoginAsync([FromBody] LogInModel model)
		{
			var user = await userManager.FindByNameAsync(model.Username);
			if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
			{
				var userRoles = await userManager.GetRolesAsync(user);

				var authClaims = new List<Claim>
					{
					new Claim(ClaimTypes.Name, user.UserName),
					new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
					};

				foreach (var userRole in userRoles)
				{
					authClaims.Add(new Claim(ClaimTypes.Role, userRole));
				}

				var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));

				var token = new JwtSecurityToken(
					issuer: _configuration["JWT:ValidIssuer"],
					audience: _configuration["JWT:ValidAudience"],
					expires: DateTime.Now.AddHours(4),
					claims: authClaims,
					signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
				);

				return Ok(new
				{
					token = new JwtSecurityTokenHandler().WriteToken(token),
					expiration = token.ValidTo
				});
			}

			return Unauthorized();
		}
	}
}
