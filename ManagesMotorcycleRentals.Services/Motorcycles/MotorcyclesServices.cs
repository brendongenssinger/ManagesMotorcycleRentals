using ManagesMotorcycleRentals.Services.Interfaces;

namespace ManagesMotorcycleRentals.Services.Motorcycles
{
    public class MotorcyclesServices : IMotorcyclesServices
    {
        public async Task<bool> CreateMotorcycleAsync()
        {
            // Implementation for creating a motorcycle
            return await Task.FromResult(true);
        }
    }
}
