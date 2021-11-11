using System;

namespace Actividades.Domain.Models
{
	public class SERSurvey
	{
		public int Id { get; set; }
		public int activity_id { get; set; }
		public string Answers { get; set; }
		public DateTime Created_at { get; set; }

		public virtual SERActivity ActivityNavigation { get; set; }
	}
}
