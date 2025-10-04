using ManagesMotorcycleRentals.Domain.Entities;
using ManagesMotorcycleRentals.Infrastructure.Context;
using ManagesMotorcycleRentals.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ManagesMotorcycleRentals.Infrastructure.Repositories
{
    public class CustomerRepositoryReadOnly : ICustomerRepositoryReadOnly
    {
        private readonly ManagesMotorcyclesRentalsDbContext _context;

        public CustomerRepositoryReadOnly(ManagesMotorcyclesRentalsDbContext managesMotorcyclesRentalsDbContext)
        {
            _context = managesMotorcyclesRentalsDbContext;
        }

        public Customer? GetCustomerByCnpj(string cnpj)
        {
            return _context.Customer.AsNoTracking().FirstOrDefault(x => x.Cnpj == cnpj);
        }

        public Task<Customer> GetCustomerByCnpjAsync(string cnpj, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
