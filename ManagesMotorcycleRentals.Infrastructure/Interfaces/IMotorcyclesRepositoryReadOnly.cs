using ManagesMotorcycleRentals.Domain.Entities;

namespace ManagesMotorcycleRentals.Infrastructure.Interfaces
{
    public interface IMotorcyclesRepositoryReadOnly
    {
        Task<Motorcycle?> GetMotorCycleByLicensePlateAsync(string licensePlate, CancellationToken cancellationToken);
        Task<List<Motorcycle?>> GetMotorCycleAll(CancellationToken cancellationToken);
    }
}
