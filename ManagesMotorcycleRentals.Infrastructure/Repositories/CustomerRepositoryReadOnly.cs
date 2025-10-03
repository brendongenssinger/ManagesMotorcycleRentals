using ManagesMotorcycleRentals.Infrastructure.Context;
using ManagesMotorcycleRentals.Infrastructure.Interfaces;

namespace ManagesMotorcycleRentals.Infrastructure.Repositories
{
    public class CustomerRepositoryReadOnly : ICustomerRepositoryReadOnly
    {
        private readonly ManagesMotorcyclesRentalsDbContext _context;

        public CustomerRepositoryReadOnly(ManagesMotorcyclesRentalsDbContext managesMotorcyclesRentalsDbContext)
        {
            _context = managesMotorcyclesRentalsDbContext;
        }


       
    }
}
