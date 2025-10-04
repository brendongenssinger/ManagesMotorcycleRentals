using ManagesMotorcycleRentals.Domain.Entities;

namespace ManagesMotorcycleRentals.Infrastructure.Interfaces
{
    public interface IPlanRentaRepositorylReadOnly
    {
        List<PlanRental?> GetPlansRentals();
        Task<List<PlanRental?>> GetPlansRentalsAsync(CancellationToken cancellationToken);
        Task<PlanRental?> GetPlansRentalsByUIdAsync(Guid uId, CancellationToken cancellationToken);
        PlanRental? GetPlansRentalsByUId(Guid uId);

    }
}
