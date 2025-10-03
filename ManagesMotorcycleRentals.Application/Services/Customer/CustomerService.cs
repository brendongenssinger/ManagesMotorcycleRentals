using Amazon.S3;
using ManagesMotorcycleRentals.Application.DTOs;
using ManagesMotorcycleRentals.Application.Services.Interfaces;
using ManagesMotorcycleRentals.Application.Services.Validator;
using ManagesMotorcycleRentals.Domain.Entities;
using ManagesMotorcycleRentals.Domain.Enumerator;
using ManagesMotorcycleRentals.Domain.Shared;
using ManagesMotorcycleRentals.Infrastructure.Interfaces;
using ManagesMotorcycleRentals.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagesMotorcycleRentals.Application.Services.Customer
{
    public class CustomerService : ServiceBase, ICustomerService
    {
        private IAmazonS3 _amazonS3;
        private CustomerServiceValidator _customerServiceValidator;
        private IUnitOfWork _unitOfWork;

        public CustomerService(Notifiable notification, CustomerServiceValidator customerServiceValidator, IAmazonS3 amazonS3, IUnitOfWork unitOfWork) : base(notification)
        {
            _amazonS3 = amazonS3;
            _customerServiceValidator = customerServiceValidator;
            _unitOfWork = unitOfWork;
        }


        public async Task<bool> CreateCustomerAsync(CreateCustomerDto customerDto, CancellationToken cancellationToken)
        {
            var validation = _customerServiceValidator.ValidatorCustomer(customerDto);
            if (validation) return false;

            var typeDocument = (TypeDocument)int.Parse(customerDto.CnhType);

            var key = Guid.NewGuid().ToString();
            var result = await _amazonS3.PutObjectAsync(new Amazon.S3.Model.PutObjectRequest()
            {
                BucketName = "motorcyclerentals",
                Key = key,
                ContentBody = customerDto.CnhImage,
                ContentType = "image/png",
                InputStream = new MemoryStream(Encoding.UTF8.GetBytes(customerDto.CnhImage))
            });

            var customer = CustomerFactory.Create(customerDto.Name, customerDto.Cnpj, customerDto.BirthDate, customerDto.CnhNumber, key, typeDocument);

            await _unitOfWork.CustomerRepository.CreateCustomerAsync(customer,cancellationToken);

            return true;

        }
    }
}
