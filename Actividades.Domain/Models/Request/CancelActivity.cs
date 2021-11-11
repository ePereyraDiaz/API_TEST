using System.Text.Json.Serialization;

namespace Actividades.Domain.Models.Request
{
	public class CancelActivity
	{
		public int Id { get; set; }

		[JsonIgnore]
		public string Status { get; set; } = "removed";
	}
}
