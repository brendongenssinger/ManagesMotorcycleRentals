using ManagesMotorcycleRentals.API.Messaging.Model;
using ManagesMotorcycleRentals.Application.Services.Interfaces;
using ManagesMotorcycleRentals.Domain.Entities;
using ManagesMotorcycleRentals.Infrastructure.Interfaces;

namespace ManagesMotorcycleRentals.Application.Services.Motorcycles
{

    public class MotocycleServiceConsumer : IMotocycleServiceConsumer
    {
        private IUnitOfWork _unitOfWork;

        public MotocycleServiceConsumer(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task SaveMotorcycleConsumer(MotorcycleMessage motorcycleMessage, CancellationToken cancellation)
        {
            var motorcycle = MotorcyleFactory.Create(motorcycleMessage.Year, motorcycleMessage.Model, motorcycleMessage.LicensePlate);
            await _unitOfWork.MotorcyclesRepository.CreateMotorcycleAsync(motorcycle, cancellation);            
            var saved = await _unitOfWork.SaveChangeAsync(cancellation);

            if(saved == 0) throw new Exception($"Error saving motorcycle\n License Plate : {motorcycle.LicensePlate}");
        }
    }
}
