using Actividades.Domain.Models;
using Actividades.Domain.Models.Request;
using Actividades.Domain.Models.Response;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Actividades.Domain.Interfaces
{
	public interface IActividades
	{
		public SERProperty GetProperty(int idProperty);

		public Activities GetActivity(int idActivity);

		public Task<List<Activities>> GetListActivitiesAsync();

		public Task<List<Activities>> GetListActivitiesFilteredAsync(Filters filters);

		public Task<bool> AddActivityAsync(AddActivity _Activity);

		public Task<bool> UpdateActivityAsync(UpdateActivity _Activity);

		public Task<bool> CancelActivityAsync(CancelActivity _Activity);
	}
}
