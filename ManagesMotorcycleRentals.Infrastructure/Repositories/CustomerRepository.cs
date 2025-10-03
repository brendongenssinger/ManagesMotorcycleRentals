using ManagesMotorcycleRentals.Domain.Entities;
using ManagesMotorcycleRentals.Infrastructure.Context;
using ManagesMotorcycleRentals.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ManagesMotorcycleRentals.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ManagesMotorcyclesRentalsDbContext _context;

        public CustomerRepository(ManagesMotorcyclesRentalsDbContext managesMotorcyclesRentalsDbContext)
        {
            _context = managesMotorcyclesRentalsDbContext;
        }

        public async Task CreateCustomerAsync(Customer customer, CancellationToken cancellationToken)
        {
            await _context.Customer.AddAsync(customer, cancellationToken);
        }
    }
}
