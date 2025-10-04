using ManagesMotorcycleRentals.Infrastructure.Context;
using ManagesMotorcycleRentals.Infrastructure.Interfaces;

namespace ManagesMotorcycleRentals.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ManagesMotorcyclesRentalsDbContext _context;
        public IMotocyclesRepository MotorcyclesRepository { get; }
        public ICustomerRepository CustomerRepository { get; }
        public IMotorcyclesAllocationsRepository MotorcyclesAllocationsRepository { get; }

        public UnitOfWork(ManagesMotorcyclesRentalsDbContext context, 
            IMotocyclesRepository motorcyclesRepository,  
            ICustomerRepository customerRepository, 
            IMotorcyclesAllocationsRepository motorcyclesAllocationsRepository
        )
        {
            _context = context;
            MotorcyclesRepository = motorcyclesRepository;
            CustomerRepository = customerRepository;
            MotorcyclesAllocationsRepository = motorcyclesAllocationsRepository;
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
