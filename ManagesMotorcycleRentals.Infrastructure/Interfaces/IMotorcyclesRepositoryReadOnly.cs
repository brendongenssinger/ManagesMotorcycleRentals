using ManagesMotorcycleRentals.Domain.Entities;

namespace ManagesMotorcycleRentals.Infrastructure.Interfaces
{
    public interface IMotorcyclesRepositoryReadOnly
    {
        Task<Motorcycle?> GetMotorCycleByLicensePlateAsync(string licensePlate, CancellationToken cancellationToken);
        Task<List<Motorcycle?>> GetMotorCycleAllAsyn(CancellationToken cancellationToken);
        Task<Motorcycle?> GetMotorCycleByUidAsync(Guid uid, CancellationToken cancellationToken);
        Motorcycle? GetMotorCycleByUid(Guid uid);
    }
}
