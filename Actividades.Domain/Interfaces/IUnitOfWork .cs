using System;
using System.Threading.Tasks;

namespace Actividades.Domain.Interfaces
{
	public interface IUnitOfWork : IDisposable
	{
		public IActividades ActividadesRepository { get; }

		int Completed();

		Task<int> CompleteAsync();
	}
}
