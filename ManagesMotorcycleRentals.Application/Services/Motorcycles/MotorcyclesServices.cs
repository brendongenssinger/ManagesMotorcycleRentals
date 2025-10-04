using ManagesMotorcycleRentals.Application.Services.Interfaces;
using ManagesMotorcycleRentals.Application.Services.Validator;
using ManagesMotorcycleRentals.Domain.Entities;
using ManagesMotorcycleRentals.Domain.Shared;
using ManagesMotorcycleRentals.DTOs;
using ManagesMotorcycleRentals.Infrastructure.Interfaces;
using RabbitMQ.Client;
using System.Text;

namespace ManagesMotorcycleRentals.Application.Services.Motorcycles
{
    public class MotorcyclesServices : ServiceBase, IMotorcyclesServices
    {
        private IMotorcyclesRepositoryReadOnly _motorcyclesRepositoryReadOnly;
        private MotorcyclesServicesValidator _motorcyclesServicesValidator;
        private IUnitOfWork _unitOfWork;

        public MotorcyclesServices(
                IMotorcyclesRepositoryReadOnly motorcyclesRepositoryReadOnly, 
                MotorcyclesServicesValidator motorcyclesServicesValidator,
                IUnitOfWork unitOfWork,
                Notify notify) : base(notify) 
        {
            _motorcyclesRepositoryReadOnly = motorcyclesRepositoryReadOnly;
            _motorcyclesServicesValidator = motorcyclesServicesValidator;
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> CreateMotorcycleAsync(MotorCycleCreateDto motorCycleDto, CancellationToken cancellationToken)
        {
            _motorcyclesServicesValidator.ValidePlate(motorCycleDto)
                .ValideYear(motorCycleDto.Year);

            if (GetNotification().HasNotifications) return false;

            var motocycle = MotorcyleFactory.Create(motorCycleDto.Year, motorCycleDto.Model, motorCycleDto.LicensePlate);

            var factory = new ConnectionFactory() { HostName = "localhost", Port = 5672, UserName = "admin", Password = "admin123" };


            using var connection = await factory.CreateConnectionAsync(cancellationToken);
            using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: "motorcycles", durable: false, exclusive: false, autoDelete: false);

            var body = Encoding.UTF8.GetBytes(System.Text.Json.JsonSerializer.Serialize(motocycle));

            await channel.BasicPublishAsync(exchange: "",  routingKey: "motorcycles",body,cancellationToken);

            return true;
        }

        public async Task<List<MotorCycleDtoResponse?>> GetMotorcyles(string licensePlate, CancellationToken cancellation)
        {
            var list = new List<Motorcycle>();

            if (string.IsNullOrEmpty(licensePlate))
            {
                list = await _motorcyclesRepositoryReadOnly.GetMotorCycleAllAsyn(cancellation);
                return list.Select(x => new MotorCycleDtoResponse
                {
                    Year = x.Year,
                    Model = x.Model,
                    LicensePlate = x?.LicensePlate ?? string.Empty,
                    Uid = x.Uid
                }).ToList();
            }
            else
            {
                var item = await _motorcyclesRepositoryReadOnly
                    .GetMotorCycleByLicensePlateAsync(licensePlate, cancellation) ?? new Motorcycle();
                
                return new List<MotorCycleDtoResponse?>()
                {
                    new MotorCycleDtoResponse
                    {
                        Uid = item.Uid,
                        Year = item.Year,
                        Model = item.Model,
                        LicensePlate = item.LicensePlate,
                    }
                };
            }   
        }

        public async Task<bool> UpdateMotorcycle(MotorCycleUpdateDto motorCycleDto, CancellationToken cancellationToken)
        {
            var motorcycle = await _motorcyclesRepositoryReadOnly
                .GetMotorCycleByLicensePlateAsync(motorCycleDto.LicensePlate, cancellationToken);

            var motorcycleNewLicensePlate = await _motorcyclesRepositoryReadOnly
                .GetMotorCycleByLicensePlateAsync(motorCycleDto.NewLicensePlate, cancellationToken);
           
            if (motorcycle == null)
            {
                AddNotification("Motorcycle", "Motorcycle not found");
                return false;
            }


            if(motorcycleNewLicensePlate != null)
            {
                AddNotification("Motorcycle", "New license plate already exists");
                return false;
            }
            
            await _unitOfWork.MotorcyclesRepository.UpdateMotorcycleAsync(motorcycle, motorCycleDto.NewLicensePlate, cancellationToken);

            await _unitOfWork.SaveChangeAsync(cancellationToken);

            return true;
        }

        public async Task<bool> DeleteMotorcycleByUId(Guid motorcycleByUId, CancellationToken cancellationToken)
        {
            
            await _unitOfWork.MotorcyclesRepository.DeleteMotorcycleAsync(motorcycleByUId, cancellationToken);
            await _unitOfWork.SaveChangeAsync(cancellationToken);
            return true;
        }
    }
}
