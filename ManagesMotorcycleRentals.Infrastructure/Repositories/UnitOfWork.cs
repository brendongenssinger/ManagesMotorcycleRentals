using ManagesMotorcycleRentals.Infrastructure.Context;
using ManagesMotorcycleRentals.Infrastructure.Interfaces;

namespace ManagesMotorcycleRentals.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ManagesMotorcyclesRentalsDbContext _context;
        public IMotocyclesRepository MotorcyclesRepository { get; }
        public UnitOfWork(IMotocyclesRepository motorcyclesRepository, ManagesMotorcyclesRentalsDbContext context)
        {
            MotorcyclesRepository = motorcyclesRepository;
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<int> SaveChangeAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);

        }
    }
}
