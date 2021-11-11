using System.ComponentModel.DataAnnotations;

namespace Actividades.Domain.Models
{
	public class RegisterModel
	{
		[Required(ErrorMessage = "Nombre de usuario obligatorio")]
		public string Username { get; set; }

		[EmailAddress]
		[Required(ErrorMessage = "Email obligatorio")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Contraseña obligatoria")]
		public string Password { get; set; }
	}
}
