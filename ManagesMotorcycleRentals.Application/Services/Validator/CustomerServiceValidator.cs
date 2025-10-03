using ManagesMotorcycleRentals.Application.DTOs;
using ManagesMotorcycleRentals.Domain.Shared;
using ManagesMotorcycleRentals.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagesMotorcycleRentals.Application.Services.Validator
{
    public class CustomerServiceValidator : ValidatorBase
    {
        private ICustomerRepositoryReadOnly _customerRepositoryReadOnly;

        public CustomerServiceValidator(Notifiable notifiable, ICustomerRepositoryReadOnly customerRepositoryReadOnly) : base(notifiable)
        {
            _customerRepositoryReadOnly = customerRepositoryReadOnly;
        }

        internal bool ValidatorCustomer(CreateCustomerDto customerDto)
        {
            var customer = _customerRepositoryReadOnly.GetCustomerByCnpj(customerDto.Cnpj);

            if(customer == null)
            {
                try
                {
                    customerDto.ValidateCnpj();
                    return true;
                }
                catch
                {
                    _notifiable.AddNotification("Cnpj", "Cnpj is invalid");
                    return false;
                }
            }
            else
            {
                _notifiable.AddNotification("Cnpj", "Cnpj already exists");
                return false;
            }
        }
    }
}
