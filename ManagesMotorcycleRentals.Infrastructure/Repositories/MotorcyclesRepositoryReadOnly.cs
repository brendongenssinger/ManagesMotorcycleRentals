using ManagesMotorcycleRentals.Domain.Entities;
using ManagesMotorcycleRentals.Infrastructure.Context;
using ManagesMotorcycleRentals.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<List<Motorcycle?>> GetMotorCycleAll(CancellationToken cancellationToken)
        {
            return await _context.Motorcycles.AsNoTracking().ToListAsync(cancellationToken) ??new List<Motorcycle>();
         }
    }
}
