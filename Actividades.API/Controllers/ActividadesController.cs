using Actividades.Domain.Models.Request;
using Actividades.Domain.Models.Response;
using Actividades.Services.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Threading.Tasks;

namespace Actividades.API.Controllers
{
	[ApiController]
	[Authorize]
	[Route("api/activities")]
	public class ActividadesController : ControllerBase
	{
		private readonly IActividadesService _ActividadesService;

		public ActividadesController(IActividadesService actividadesService)
		{
			this._ActividadesService = actividadesService ?? throw new ArgumentNullException(nameof(actividadesService));
		}

		[HttpGet("activities")]
		public async Task<ActionResult> GetActivitiesAsync()
		{
			try
			{
				var activities = await _ActividadesService.GetActivitiesAsync();
				return Ok(activities);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				return StatusCode(500, ex.Message);
			}
		}

		[HttpPost("filteredActivities")]
		public async Task<ActionResult> GetActivitiesAsync([FromBody] Filters filters)
		{
			try
			{
				var activities = await _ActividadesService.GetListActivitiesFilteredAsync(filters);
				return Ok(activities);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				return StatusCode(500, ex.Message);
			}
		}

		[HttpPost("addActivity")]
		public async Task<ActionResult> AddActivityAsync([FromBody] AddActivity _Activity)
		{
			try
			{
				var activity = new GenericResponse();
				var Property = _ActividadesService.GetProperty(_Activity.property_id);

				if (Property.Status != "removed")
				{
					var Activities_ = await _ActividadesService.GetListActivitiesFilteredAsync(
						new Filters
						{
							StartDate = _Activity.Schedule.AddHours(-1),
							EndDate = _Activity.Schedule,
							Status = "active"
						});

					if (Activities_.Count > 0)
					{
						activity.Message = "No es posible crear una actividad con la misma fecha y hora";
					}
					else
					{
						activity.Result = await _ActividadesService.AddActivityAsync(_Activity);
						activity.Message = "Actividad creada correctamente";
					}
				}
				else
				{
					activity.Message = "La propiedad que desea ligar tiene un estatus incorrecto: " + Property.Status;
				}

				return Ok(activity);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				return StatusCode(500, ex.Message);
			}
		}

		[HttpPut("updateActivity")]
		public async Task<ActionResult> UpdateActivityAsync([FromBody] UpdateActivity _Activity)
		{
			try
			{
				var activity = new GenericResponse();

				var Activities_ = _ActividadesService.GetActivity(_Activity.Id);

				if (Activities_.status != "removed")
				{
					activity.Result = await _ActividadesService.UpdateActivityAsync(_Activity);
					activity.Message = "La actividad se reagendo correctamente";
				}
				else
				{
					activity.Message = "No es posible reagendar la actividad porque está cancelada";
				}

				return Ok(activity);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				return StatusCode(500, ex.Message);
			}
		}

		[HttpPut("cancelActivity")]
		public async Task<ActionResult> CancelActivityAsync([FromBody] CancelActivity _Activity)
		{
			try
			{
				var activity = new GenericResponse();
				var Activities_ = _ActividadesService.GetActivity(_Activity.Id);

				if (Activities_ == null)
				{
					activity.Message = "La actividad que desea cancelar no existe";
				}
				else
				{
					activity.Result = await _ActividadesService.CancelActivityAsync(_Activity);
					activity.Message = "La actividad se cancelo correctamente";
				}

				return Ok(activity);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				return StatusCode(500, ex.Message);
			}
		}
	}
}
