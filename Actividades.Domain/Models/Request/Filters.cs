using System;

namespace Actividades.Domain.Models.Request
{
	public class Filters
	{
		public DateTime StartDate { get; set; } = DateTime.MinValue;

		public DateTime EndDate { get; set; } = DateTime.MinValue;

		public string Status { get; set; } = "";

	}
}
