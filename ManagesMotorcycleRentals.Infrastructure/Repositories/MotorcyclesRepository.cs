using ManagesMotorcycleRentals.Domain.Entities;
using ManagesMotorcycleRentals.Infrastructure.Context;
using ManagesMotorcycleRentals.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ManagesMotorcycleRentals.Infrastructure.Repositories
{
    public class MotorcyclesRepository : IMotocyclesRepository
    {
        private readonly ManagesMotorcyclesRentalsDbContext _context;
        public MotorcyclesRepository(ManagesMotorcyclesRentalsDbContext managesMotorcyclesRentalsDbContext)
        {
            _context = managesMotorcyclesRentalsDbContext;
        }
        public async Task CreateMotorcycleAsync(Motorcycle motorcycle, CancellationToken cancellationToken)
        {
            try
            {
                await _context.Motorcycles.AddAsync(motorcycle, cancellationToken);
            }
            catch (Exception eX)
            {
                throw eX;
            }
        }

        public async Task UpdateMotorcycleAsync(Motorcycle motorcycle, string newLicensePlate, CancellationToken cancellationToken)
        {
            await (from item in _context.Motorcycles.AsNoTracking()
                   where item.LicensePlate == motorcycle.LicensePlate
                   select item)
                   .ExecuteUpdateAsync(x => x.SetProperty(p => p.LicensePlate, newLicensePlate), cancellationToken);
        }

        public async Task DeleteMotorcycleAsync(Guid uid, CancellationToken cancellationToken)
        {
            await (from item in _context.Motorcycles.AsNoTracking()
                   where item.Uid ==  uid
                   select item)
                   .ExecuteDeleteAsync(cancellationToken);
        }
    }
}
