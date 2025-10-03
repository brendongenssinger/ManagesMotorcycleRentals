using ManagesMotorcycleRentals.Domain.Shared;
using ManagesMotorcycleRentals.DTOs;
using ManagesMotorcycleRentals.Infrastructure.Interfaces;
using Microsoft.VisualBasic;
using System.Threading;

namespace ManagesMotorcycleRentals.Application.Services.Validator
{
    public class MotorcyclesServicesValidator: ValidatorBase
    {
        private IMotorcyclesRepositoryReadOnly _motorcyclesRepositoryReadOnly;
        public MotorcyclesServicesValidator(IMotorcyclesRepositoryReadOnly motorcyclesRepositoryReadOnly, Notifiable notifiable) : base(notifiable)
        {
            _motorcyclesRepositoryReadOnly = motorcyclesRepositoryReadOnly;
        }
        public async Task<MotorcyclesServicesValidator> ValidCreateMotorcycle(MotorCycleCreateDto motorCycleDto, CancellationToken cancellationToken)
        {
            var motorCycle = await _motorcyclesRepositoryReadOnly
                    .GetMotorCycleByLicensePlateAsync(
                           motorCycleDto.LicensePlate, cancellationToken);

            var teste = await _motorcyclesRepositoryReadOnly.GetMotorCycleAll(cancellationToken);
            if(motorCycle != null)
                _notifiable.AddNotification("motorCycle", "Motorcycle already exists");

            return await Task.Run(() => this);
        }

        public MotorcyclesServicesValidator ValidePlate(MotorCycleCreateDto motorCycleDto)
        {

            if(string.IsNullOrEmpty(motorCycleDto.LicensePlate))
                _notifiable.AddNotification("LicensePlate", "License Plate is required");
            else if(motorCycleDto.LicensePlate.Length < 7)
                _notifiable.AddNotification("LicensePlate", "License Plate must be 7 characters long");

            ValidCreateMotorcycle(motorCycleDto, CancellationToken.None).Wait();            


            return this;
        }

        public MotorcyclesServicesValidator ValideYear(int year)
        {
            if(year < 1900 || year > DateTime.Now.Year +1)
                _notifiable.AddNotification("Year", "Year is invalid");
            return this;
        }
    }
}
