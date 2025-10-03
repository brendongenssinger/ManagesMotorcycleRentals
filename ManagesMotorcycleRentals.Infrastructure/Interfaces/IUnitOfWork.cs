using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagesMotorcycleRentals.Infrastructure.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IMotocyclesRepository MotorcyclesRepository { get; }
        ICustomerRepository CustomerRepository { get; }
        Task<int> SaveChangeAsync(CancellationToken cancellationToken);
    }
}
