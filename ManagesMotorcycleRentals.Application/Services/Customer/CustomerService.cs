using Amazon.S3;
using ManagesMotorcycleRentals.Application.DTOs;
using ManagesMotorcycleRentals.Application.Services.Interfaces;
using ManagesMotorcycleRentals.Application.Services.Validator;
using ManagesMotorcycleRentals.Domain.Entities;
using ManagesMotorcycleRentals.Domain.Enumerator;
using ManagesMotorcycleRentals.Domain.Shared;
using ManagesMotorcycleRentals.Infrastructure.Interfaces;
using System.Text;

namespace ManagesMotorcycleRentals.Application.Services.Customer
{
    public class CustomerService : ServiceBase, ICustomerService
    {
        private IAmazonS3 _amazonS3;
        private CustomerServiceValidator _customerServiceValidator;
        private IUnitOfWork _unitOfWork;

        public CustomerService(
            Notify notification, 
            CustomerServiceValidator customerServiceValidator, 
            IAmazonS3 amazonS3, 
            IUnitOfWork unitOfWork) : base(notification)
        {
            _amazonS3 = amazonS3;
            _customerServiceValidator = customerServiceValidator;
            _unitOfWork = unitOfWork;
        }


        public async Task<bool> CreateCustomerAsync(CreateCustomerDto customerDto, CancellationToken cancellationToken)
        {
            _customerServiceValidator.ValidatorCustomer(customerDto);
            if (GetNotification().HasNotifications) return false;


            var typeDocument = (TypeDocument)customerDto.CnhType;

            var key = Guid.NewGuid().ToString();

            await _amazonS3.EnsureBucketExistsAsync("motorcyclerentals");

            var result = await _amazonS3.PutObjectAsync(new Amazon.S3.Model.PutObjectRequest()
            {
                BucketName = "motorcyclerentals",
                Key = key,
                ContentBody = customerDto.CnhImage,
                ContentType = "image/png",
                InputStream = customerDto.CnhImageFormData.OpenReadStream()
            });

            var customer = CustomerFactory.Create(customerDto.Name, customerDto.Cnpj, customerDto.BirthDate, customerDto.CnhNumber, key, typeDocument);

            await _unitOfWork.CustomerRepository.CreateCustomerAsync(customer,cancellationToken);
            await _unitOfWork.SaveChangeAsync(cancellationToken);

            return true;

        }

        public Task<object> CreateCustomerRental(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CreateCustomerRentalMotorcycleAsync(CreateCustomerRentalMotorcycleDto createCustomerRentalMotorcycleDto, CancellationToken cancellationToken)
        {
            _customerServiceValidator.ValidatorCustomerRental(createCustomerRentalMotorcycleDto);
            if (GetNotification().HasNotifications)
                return false;

            var motorcyclesAllocation = MotorcyclesAllocationsFactory.Create(
                    startDate: createCustomerRentalMotorcycleDto.GetDateTimeStart(),
                    endDate: createCustomerRentalMotorcycleDto.RentalEndDate ?? DateTime.MinValue,
                    expectedReturnDate: createCustomerRentalMotorcycleDto.ExpectedReturnDate ?? DateTime.MinValue,
                    totalCost: createCustomerRentalMotorcycleDto.GetPlanRental().TotalPrice,
                    motorCycleId: createCustomerRentalMotorcycleDto.GetMotorcycle().Id,
                    customerId: createCustomerRentalMotorcycleDto.GetCustomer().Id,
                    planRentalId: createCustomerRentalMotorcycleDto.GetPlanRental().Id);

            await _unitOfWork.MotorcyclesAllocationsRepository.CreateMotorcycleAllocationsAsync(motorcyclesAllocation, cancellationToken);
            await _unitOfWork.SaveChangeAsync(cancellationToken);
            return true;

        }
    }
}
