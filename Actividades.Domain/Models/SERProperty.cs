using System;
using System.Collections.Generic;

namespace Actividades.Domain.Models
{
	public class SERProperty
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Address { get; set; }
		public string Description { get; set; }
		public DateTime Created_at { get; set; }
		public DateTime Updated_at { get; set; }
		public DateTime? Disabled_at { get; set; }
		public string Status { get; set; }

		public virtual ICollection<SERActivity> Activity { get; set; }
	}
}
