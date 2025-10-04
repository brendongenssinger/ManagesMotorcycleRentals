using ManagesMotorcycleRentals.Domain.Entities;
using ManagesMotorcycleRentals.Infrastructure.Context;
using ManagesMotorcycleRentals.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ManagesMotorcycleRentals.Infrastructure.Repositories
{
    public class PlanRentaRepositorylReadOnly : IPlanRentaRepositorylReadOnly
    {
        private readonly ManagesMotorcyclesRentalsDbContext _context;

        
        public PlanRentaRepositorylReadOnly(ManagesMotorcyclesRentalsDbContext managesMotorcyclesRentalsDbContext)
        {
            _context = managesMotorcyclesRentalsDbContext;
        }


        public async Task<List<PlanRental?>> GetPlansRentals(CancellationToken cancellationToken)
        {
            return await _context.PlanRentals.AsNoTracking().ToListAsync(cancellationToken) ?? new List<PlanRental>();                
        }

        public List<PlanRental> GetPlansRentals()
        {
            return _context.PlanRentals.AsNoTracking().ToList() ?? new List<PlanRental>();
        }

        public async Task<List<PlanRental?>> GetPlansRentalsAsync(CancellationToken cancellationToken)
        {
            try
            {
                return await _context.PlanRentals.AsNoTracking().ToListAsync(cancellationToken) ?? new List<PlanRental>();
            }
            catch (Exception eX)
            {
                return null;
            }
        }

        public async Task<PlanRental?> GetPlansRentalsByUId(Guid uid, CancellationToken cancellationToken)
        {
            return await _context.PlanRentals.AsNoTracking().FirstOrDefaultAsync(x => x.Uid == uid, cancellationToken);
        }

        public PlanRental? GetPlansRentalsByUId(Guid uId)
        {
            return _context.PlanRentals.AsNoTracking().FirstOrDefault(x => x.Uid == uId);
        }

        public async Task<PlanRental?> GetPlansRentalsByUIdAsync(Guid uId, CancellationToken cancellationToken)
        {
            return await _context.PlanRentals.AsNoTracking().FirstOrDefaultAsync(x => x.Uid == uId, cancellationToken);
        }
    }
}
