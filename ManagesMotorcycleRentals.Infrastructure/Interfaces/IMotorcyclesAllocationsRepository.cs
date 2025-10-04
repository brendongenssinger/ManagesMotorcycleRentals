using ManagesMotorcycleRentals.Domain.Entities;

namespace ManagesMotorcycleRentals.Infrastructure.Interfaces
{
    public interface IMotorcyclesAllocationsRepository
    {
        public Task CreateMotorcycleAllocationsAsync(Domain.Entities.MotorcyclesAllocations motorcyclesAllocations, CancellationToken cancellationToken);
    }
}
