using ManagesMotorcycleRentals.Domain.Entities;
using ManagesMotorcycleRentals.Infrastructure.Context;
using ManagesMotorcycleRentals.Infrastructure.Interfaces;

namespace ManagesMotorcycleRentals.Infrastructure.Repositories
{
    public class MotorcyclesAllocationsRepository : IMotorcyclesAllocationsRepository
    {
        private readonly ManagesMotorcyclesRentalsDbContext _context;
        public MotorcyclesAllocationsRepository(ManagesMotorcyclesRentalsDbContext managesMotorcyclesRentalsDbContext)
        {
            _context = managesMotorcyclesRentalsDbContext;
        }

        public async Task CreateMotorcycleAllocationsAsync(MotorcyclesAllocations motorcyclesAllocations, CancellationToken cancellationToken)
        {
            await _context.MotorcyclesAllocations.AddAsync(motorcyclesAllocations, cancellationToken);
        }
    }
}
