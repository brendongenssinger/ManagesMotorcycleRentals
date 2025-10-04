using ManagesMotorcycleRentals.Application.DTOs;

namespace ManagesMotorcycleRentals.Application.Services.Interfaces
{
    public interface IPlanRentalsService
    {
        Task<List<PlansRentalDto>> GetPlansRentalsAsync(CancellationToken cancellationToken);
        Task<PlansRentalDto> GetPlansRentalsByUIdAsync(Guid uid, CancellationToken cancellationToken);
    }
}
