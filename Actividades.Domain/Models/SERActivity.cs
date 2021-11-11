using System;
using System.Collections.Generic;

namespace Actividades.Domain.Models
{
	public class SERActivity
	{
		public int Id { get; set; }
		public int property_id { get; set; }
		public DateTime Schedule { get; set; }
		public string Title { get; set; }
		public DateTime Created_at { get; set; }
		public DateTime Updated_at { get; set; }
		public string Status { get; set; }

		public virtual SERProperty PropertyNavigation { get; set; }

		public virtual ICollection<SERSurvey> Survey { get; set; }
	}
}
