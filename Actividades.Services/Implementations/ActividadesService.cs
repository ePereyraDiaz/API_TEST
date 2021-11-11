using Actividades.Domain.Interfaces;
using Actividades.Domain.Models;
using Actividades.Domain.Models.Request;
using Actividades.Domain.Models.Response;
using Actividades.Services.Interfaces;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Actividades.Services.Implementations
{
	public class ActividadesService : IActividadesService
	{
		private readonly IUnitOfWork _unitOfWork;
		/// <summary>
		/// ctor
		/// </summary>
		/// <param name="unitOfWork"></param>
		/// <exception cref="ArgumentNullException"></exception>
		public ActividadesService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
		}

		public SERProperty GetProperty(int idProperty)
		{
			return _unitOfWork.ActividadesRepository.GetProperty(idProperty);
		}

		public Activities GetActivity(int idActivity)
		{
			return _unitOfWork.ActividadesRepository.GetActivity(idActivity);
		}

		public async Task<List<Activities>> GetActivitiesAsync()
		{
			return await _unitOfWork.ActividadesRepository.GetListActivitiesAsync();
		}

		public async Task<List<Activities>> GetListActivitiesFilteredAsync(Filters filters)
		{
			return await _unitOfWork.ActividadesRepository.GetListActivitiesFilteredAsync(filters);
		}

		public async Task<bool> AddActivityAsync(AddActivity _Activity)
		{
			try
			{
				_Activity.Created_at = DateTime.Now;
				_Activity.Updated_at = DateTime.Now;
				return await _unitOfWork.ActividadesRepository.AddActivityAsync(_Activity);
			}
			catch (Exception)
			{
				return false;
			}
		}

		public async Task<bool> CancelActivityAsync(CancelActivity _Activity)
		{
			try
			{
				return await _unitOfWork.ActividadesRepository.CancelActivityAsync(_Activity);
			}
			catch (Exception)
			{
				return false;
			}
		}


		public async Task<bool> UpdateActivityAsync(UpdateActivity _Activity)
		{
			try
			{
				return await _unitOfWork.ActividadesRepository.UpdateActivityAsync(_Activity);
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}
