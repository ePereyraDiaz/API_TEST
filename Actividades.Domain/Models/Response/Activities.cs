using System;

namespace Actividades.Domain.Models.Response
{
	public class Activities
	{
		public int ID { get; set; }
		public DateTime schedule { get; set; }
		public string title { get; set; }
		public DateTime created_at { get; set; }
		public string status { get; set; }
		public string condition { get; set; }
		public propertyModel property { get; set; }
		public string Survey { get; set; }
	}

	public class propertyModel
	{
		public int ID { get; set; }
		public string title { get; set; }
		public string address { get; set; }
	}
}
