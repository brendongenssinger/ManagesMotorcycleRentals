using ManagesMotorcycleRentals.Domain.Entities;

namespace ManagesMotorcycleRentals.Infrastructure.Interfaces
{
    public interface IMotocyclesRepository
    {
        public Task CreateMotorcycleAsync(Domain.Entities.Motorcycle motorcycle, CancellationToken cancellationToken);
        public Task UpdateMotorcycleAsync(Motorcycle motorcycle, string newLicensePlate, CancellationToken cancellationToken);
        Task DeleteMotorcycleAsync(Guid uid, CancellationToken cancellationToken);
    }
}
