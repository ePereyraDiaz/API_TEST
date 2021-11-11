using System;

namespace Actividades.Domain.Models.Request
{
	public class UpdateActivity
	{
		public int Id { get; set; }
		public DateTime Schedule { get; set; }
	}
}
