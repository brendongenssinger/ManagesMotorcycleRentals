using ManagesMotorcycleRentals.DTOs;

namespace ManagesMotorcycleRentals.Application.Services.Interfaces
{
    public interface IMotorcyclesServices
    {
        Task<bool> CreateMotorcycleAsync(MotorCycleCreateDto motorCycleDto, CancellationToken cancellationToken);
        Task<List<MotorCycleCreateDto?>> GetMotorcyles(string licensePlate, CancellationToken cancellation);
        Task<bool> UpdateMotorcycle(MotorCycleUpdateDto motorCycleDto, CancellationToken cancellationToken);
        Task<bool> DeleteMotorcycleByUId(Guid motorcycleByUId, CancellationToken cancellationToken);
    }
}
