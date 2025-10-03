using ManagesMotorcycleRentals.Domain.Entities;

namespace ManagesMotorcycleRentals.Infrastructure.Interfaces
{
    public interface ICustomerRepositoryReadOnly
    {
        Customer GetCustomerByCnpj(string cnpj);
        Task<Customer> GetCustomerByCnpjAsync(string cnpj, CancellationToken cancellationToken);
    }
}
