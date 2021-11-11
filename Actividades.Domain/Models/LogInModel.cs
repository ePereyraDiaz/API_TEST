using System.ComponentModel.DataAnnotations;

namespace Actividades.Domain.Models
{
	public class LogInModel
	{
		[Required(ErrorMessage = "Nombre de usuario obligatorio")]
		public string Username { get; set; }

		[Required(ErrorMessage = "Contraseña obligatoria")]
		public string Password { get; set; }

	}
}
