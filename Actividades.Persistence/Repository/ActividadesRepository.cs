using Actividades.Domain.Interfaces;
using Actividades.Domain.Models;
using Actividades.Domain.Models.Request;
using Actividades.Domain.Models.Response;
using Actividades.Persistence.DB_Context;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Actividades.Persistence.Repository
{
	public class ActividadesRepository : IActividades
	{
		private readonly ActivitiesDbContext _DbContext;

		/// <summary>
		/// ctor
		/// </summary>
		/// <param name="context"></param>
		/// <exception cref="ArgumentNullException"></exception>
		public ActividadesRepository(ActivitiesDbContext context)
		{
			_DbContext = context ?? throw new ArgumentNullException(nameof(context));
		}

		public static readonly Predicate<Filters>[] validateFilters =
		{
			(a) => a.StartDate > DateTime.MinValue,
			(a) => a.EndDate > DateTime.MinValue,
			(a) => a.Status != ""
		};

		public static bool ModelValidator<T>(Predicate<T>[] validates, T model)
		{
			var errors = validates.Where(x => !x(model)).Count();

			return errors == 0;
		}

		public Activities GetActivity(int idActivity)
		{
			try
			{
				var result = (from activity in _DbContext.SERActivity
							  where activity.Id == idActivity
							  select new Activities
							  {
								  ID = activity.Id,
								  schedule = activity.Schedule,
								  title = activity.Title,
								  created_at = activity.Created_at,
								  status = activity.Status
							  }).FirstOrDefault();
				return result;
			}
			catch (Exception)
			{
				return new Activities();
			}
		}

		public SERProperty GetProperty(int idProperty)
		{
			try
			{
				var result = (from property in _DbContext.SERProperty
							  where property.Id == idProperty
							  select property).FirstOrDefault();
				return result;
			}
			catch (Exception)
			{
				return new SERProperty();
			}
		}

		public async Task<List<Activities>> GetListActivitiesAsync()
		{
			try
			{
				var result = await (from activity in _DbContext.SERActivity
									join property in _DbContext.SERProperty
									on activity.property_id equals property.Id
									where DateTime.Now.AddDays(-3) <= activity.Schedule && activity.Schedule <= DateTime.Now.AddDays(14)
									orderby activity.Id ascending
									select new Activities
									{
										ID = activity.Id,
										schedule = activity.Schedule,
										title = activity.Title,
										created_at = activity.Created_at,
										status = activity.Status,
										condition =
										(activity.Status == "active" && activity.Schedule >= DateTime.Now ? "Pendiente a realizar" :
										 activity.Status == "active" && activity.Schedule < DateTime.Now ? "Atrasada" :
										 activity.Status == "done" && activity.Schedule < DateTime.Now ? "Finalizada" :
										""),
										property = new propertyModel
										{
											ID = property.Id,
											title = property.Title,
											address = property.Address
										},
										Survey = "http://nssoftware.mx/" + property.Address + property.Id
									}).ToListAsync();
				return result;
			}
			catch (Exception)
			{
				return new List<Activities>();
			}
		}

		public async Task<List<Activities>> GetListActivitiesFilteredAsync(Filters filters)
		{
			try
			{
				var result = await (from activity in _DbContext.SERActivity
									join property in _DbContext.SERProperty
									on activity.property_id equals property.Id
									where filters.StartDate <= activity.Schedule && activity.Schedule <= filters.EndDate && activity.Status == filters.Status
									orderby activity.Id ascending
									select new Activities
									{
										ID = activity.Id,
										schedule = activity.Schedule,
										title = activity.Title,
										created_at = activity.Created_at,
										status = activity.Status,
										condition =
										(activity.Status == "active" && activity.Schedule >= DateTime.Now ? "Pendiente a realizar" :
										 activity.Status == "active" && activity.Schedule < DateTime.Now ? "Atrasada" :
										 activity.Status == "done" && activity.Schedule < DateTime.Now ? "Finalizada" :
										""),
										property = new propertyModel
										{
											ID = property.Id,
											title = property.Title,
											address = property.Address
										},
										Survey = "http://nssoftware.mx/" + property.Address + property.Id
									}).ToListAsync();
				return result;

			}
			catch (Exception ex)
			{
				return new List<Activities>();
			}
		}

		public async Task<bool> AddActivityAsync(AddActivity _activity)
		{
			try
			{
				_DbContext.SERActivity.Add(
					new SERActivity
					{
						Created_at = _activity.Created_at,
						property_id = _activity.property_id,
						Schedule = _activity.Schedule,
						Status = _activity.Status,
						Title = _activity.Title,
						Updated_at = DateTime.Now
					});

				await _DbContext.SaveChangesAsync();

				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public async Task<bool> UpdateActivityAsync(UpdateActivity _activity)
		{
			try
			{
				using (_DbContext)
				{
					var entity = _DbContext.SERActivity.FirstOrDefault(item => item.Id == _activity.Id);
					if (entity != null)
					{
						entity.Schedule = _activity.Schedule;
						entity.Updated_at = DateTime.Now;

						await _DbContext.SaveChangesAsync();

						return true;
					}

					return false;
				}
			}
			catch (Exception)
			{
				return false;
			}
		}

		public async Task<bool> CancelActivityAsync(CancelActivity _activity)
		{
			try
			{
				using (_DbContext)
				{
					var entity = _DbContext.SERActivity.FirstOrDefault(item => item.Id == _activity.Id);
					if (entity != null)
					{
						entity.Status = _activity.Status;
						entity.Updated_at = DateTime.Now;

						await _DbContext.SaveChangesAsync();

						return true;
					}

					return false;
				}
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}
