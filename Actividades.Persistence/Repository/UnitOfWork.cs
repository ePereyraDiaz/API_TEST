using Actividades.Domain.Interfaces;
using Actividades.Persistence.DB_Context;

using System.Threading.Tasks;

namespace Actividades.Persistence.Repository
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ActivitiesDbContext _context;
		private IActividades _Actividades;

		public UnitOfWork(ActivitiesDbContext context)
		{
			_context = context;
		}

		public IActividades ActividadesRepository
		{
			get { return _Actividades ??= new ActividadesRepository(_context); }
		}

		public Task<int> CompleteAsync()
		{
			return _context.SaveChangesAsync();
		}

		public int Completed()
		{
			return _context.SaveChanges();
		}

		public void Dispose()
		{
			_context.Dispose();
		}
	}
}
