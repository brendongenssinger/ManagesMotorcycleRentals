using ManagesMotorcycleRentals.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagesMotorcycleRentals.Application.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<bool> CreateCustomerAsync(CreateCustomerDto customerDto, CancellationToken cancellationToken);
        Task<object> CreateCustomerRental(CancellationToken cancellationToken);
        Task<bool> CreateCustomerRentalMotorcycleAsync(CreateCustomerRentalMotorcycleDto createCustomerRentalMotorcycleDto, CancellationToken cancellationToken);
    }
}
