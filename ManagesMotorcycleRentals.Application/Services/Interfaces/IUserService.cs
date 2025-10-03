namespace ManagesMotorcycleRentals.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<string> GenerateTokenAsync(int userId, string role, CancellationToken cancellationToken);
    }
}
