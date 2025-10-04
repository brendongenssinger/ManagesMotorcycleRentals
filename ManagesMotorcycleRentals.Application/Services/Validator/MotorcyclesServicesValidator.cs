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
        public MotorcyclesServicesValidator(IMotorcyclesRepositoryReadOnly motorcyclesRepositoryReadOnly, Notify notify) : base(notify)
        {
            _motorcyclesRepositoryReadOnly = motorcyclesRepositoryReadOnly;
        }
        public async Task<MotorcyclesServicesValidator> ValidCreateMotorcycle(MotorCycleCreateDto motorCycleDto, CancellationToken cancellationToken)
        {
            var motorCycle = await _motorcyclesRepositoryReadOnly
                    .GetMotorCycleByLicensePlateAsync(
                           motorCycleDto.LicensePlate, cancellationToken);

            var teste = await _motorcyclesRepositoryReadOnly.GetMotorCycleAllAsyn(cancellationToken);
            if(motorCycle != null)
                _notification.AddNotification("motorCycle", "Motorcycle already exists");

            return await Task.Run(() => this);
        }

        public MotorcyclesServicesValidator ValidePlate(MotorCycleCreateDto motorCycleDto)
        {

            if(string.IsNullOrEmpty(motorCycleDto.LicensePlate))
                _notification.AddNotification("LicensePlate", "License Plate is required");
            else if(motorCycleDto.LicensePlate.Length < 7)
                _notification.AddNotification("LicensePlate", "License Plate must be 7 characters long");

            ValidCreateMotorcycle(motorCycleDto, CancellationToken.None).Wait();            


            return this;
        }

        public MotorcyclesServicesValidator ValideYear(int year)
        {
            if(year < 1900 || year > DateTime.Now.Year +1)
                _notification.AddNotification("Year", "Year is invalid");
            return this;
        }
    }
}
