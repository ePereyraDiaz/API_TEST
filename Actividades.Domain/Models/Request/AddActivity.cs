using System;
using System.Text.Json.Serialization;

namespace Actividades.Domain.Models.Request
{
	public class AddActivity
	{
		public int property_id { get; set; }

		public DateTime Schedule { get; set; }

		public string Title { get; set; }
		
		[JsonIgnore]
		public DateTime Created_at { get; set; }
		
		[JsonIgnore]
		public DateTime Updated_at { get; set; }

		public string Status { get; set; }
	}
}
