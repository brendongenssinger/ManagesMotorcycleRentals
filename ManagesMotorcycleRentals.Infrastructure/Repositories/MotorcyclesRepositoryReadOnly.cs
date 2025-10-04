using ManagesMotorcycleRentals.Domain.Entities;
using ManagesMotorcycleRentals.Infrastructure.Context;
using ManagesMotorcycleRentals.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ManagesMotorcycleRentals.Infrastructure.Repositories
{
    public class MotorcyclesRepositoryReadOnly : IMotorcyclesRepositoryReadOnly
    {
        private readonly ManagesMotorcyclesRentalsDbContext _context;

        public MotorcyclesRepositoryReadOnly(ManagesMotorcyclesRentalsDbContext managesMotorcyclesRentalsDbContext)
        {
            _context = managesMotorcyclesRentalsDbContext;
        }


        public async Task<Motorcycle?> GetMotorCycleByLicensePlateAsync(string licensePlate, CancellationToken cancellationToken)
        {
            return await _context.Motorcycles.AsNoTracking()
                .FirstOrDefaultAsync(m => m.LicensePlate == licensePlate, cancellationToken);
        }

        public async Task<List<Motorcycle?>> GetMotorCycleAllAsyn(CancellationToken cancellationToken)
        {
            return await _context.Motorcycles.AsNoTracking().ToListAsync(cancellationToken) ??new List<Motorcycle>();
        }

        public async Task<Motorcycle?> GetMotorCycleByUidAsync(Guid uid, CancellationToken cancellationToken)
        {
            return await _context.Motorcycles.AsNoTracking().Where(x => x.Uid == uid).FirstOrDefaultAsync(cancellationToken);
        }

        public Motorcycle? GetMotorCycleByUid(Guid uid)
        {
            return _context.Motorcycles.AsNoTracking().Where(x => x.Uid == uid).FirstOrDefault();
        }
    }
}
