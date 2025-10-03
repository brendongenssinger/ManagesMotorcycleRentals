namespace ManagesMotorcycleRentals.Infrastructure.Interfaces
{
    public interface ICustomerRepository
    {
        public Task CreateCustomerAsync(Domain.Entities.Customer customer, CancellationToken cancellationToken);
    }
}
