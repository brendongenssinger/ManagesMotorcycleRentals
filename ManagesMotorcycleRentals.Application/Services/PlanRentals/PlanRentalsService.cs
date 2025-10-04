using ManagesMotorcycleRentals.Application.DTOs;
using ManagesMotorcycleRentals.Application.Services.Interfaces;
using ManagesMotorcycleRentals.Domain.Shared;
using ManagesMotorcycleRentals.Infrastructure.Interfaces;

namespace ManagesMotorcycleRentals.Application.Services.PlanRentals
{
    public class PlanRentalsService : ServiceBase, IPlanRentalsService
    {
        private readonly IPlanRentaRepositorylReadOnly _planRentalReadOnly;

        public PlanRentalsService(Notify notify, IPlanRentaRepositorylReadOnly planRentalReadOnly ) : base(notify)
        {
            _planRentalReadOnly = planRentalReadOnly;
        }

        public async Task<List<PlansRentalDto>> GetPlansRentalsAsync(CancellationToken cancellationToken)
        {
            var result = await _planRentalReadOnly.GetPlansRentalsAsync(cancellationToken);
            return result.Select(x => new PlansRentalDto
            {
                Uid = x.Uid,                                
                PricePerDay = x.PricePerDay,
                Days = x.Days,
                TotalPrice = x.TotalPrice

           }).ToList();            
        }

        public async Task<PlansRentalDto> GetPlansRentalsByUIdAsync(Guid uid, CancellationToken cancellationToken)
        {
            var result = await _planRentalReadOnly.GetPlansRentalsByUIdAsync(uid, cancellationToken);
            return new PlansRentalDto()
            {
                TotalPrice = result.TotalPrice,
                Days = result.Days,
                PricePerDay = result.PricePerDay,
                Uid = result.Uid
            };
           
        }
    }
}
