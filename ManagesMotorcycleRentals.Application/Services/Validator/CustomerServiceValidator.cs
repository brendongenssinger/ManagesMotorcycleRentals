using ManagesMotorcycleRentals.Application.DTOs;
using ManagesMotorcycleRentals.Domain.Enumerator;
using ManagesMotorcycleRentals.Domain.Shared;
using ManagesMotorcycleRentals.Infrastructure.Interfaces;
using ManagesMotorcycleRentals.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagesMotorcycleRentals.Application.Services.Validator
{
    public class CustomerServiceValidator : ValidatorBase
    {
        private readonly ICustomerRepositoryReadOnly _customerRepositoryReadOnly;
        private readonly IPlanRentaRepositorylReadOnly _planRentalReadOnly;
        public readonly IMotorcyclesRepositoryReadOnly _motorcyclesRepositoryReadOnly;

        private static readonly int[] Pesos1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        private static readonly int[] Pesos2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };


        public CustomerServiceValidator(Notify notify, 
            ICustomerRepositoryReadOnly customerRepositoryReadOnly,
            IPlanRentaRepositorylReadOnly planRentalReadOnly, 
            IMotorcyclesRepositoryReadOnly motorcyclesRepositoryReadOnly) : base(notify)
        {
            _customerRepositoryReadOnly = customerRepositoryReadOnly;
            _planRentalReadOnly = planRentalReadOnly;
            _motorcyclesRepositoryReadOnly = motorcyclesRepositoryReadOnly;
        }

        internal void ValidatorCustomer(CreateCustomerDto customerDto)
        {
            var customer = _customerRepositoryReadOnly.GetCustomerByCnpj(customerDto.Cnpj);

            if(customer == null)
            {
                ValidateCnpj(customerDto.Cnpj);
                ValidateTypeDocument(customerDto.CnhType);
                ValidImage(customerDto.CnhImageFormData);
                ValidFormatImage(customerDto.CnhImageFormData);
            }
            else
            {
                _notification.AddNotification("Customer", "Customer already added");
                
            }
        }

        private void ValidFormatImage(IFormFile? cnhImageFormData)
        {
            var png = cnhImageFormData.FileName.ToLower().EndsWith(".png");
            var bmp = cnhImageFormData.FileName.ToLower().EndsWith(".bmp");
            if (png && bmp)
                throw new ArgumentException("Image format invalid. Only .png or .bmp are allowed");
        }

        public void ValidateCnpj(string cnpj)
        {
            if (cnpj.Length != 14 || !cnpj.All(char.IsDigit))
            {
                _notification.AddNotification("cnpj", "CNPJ deve conter exatamente 14 dígitos numéricos.");
            }

            if (!IsValid(cnpj))
            {
                _notification.AddNotification(cnpj, "CNPJ incorrect.");
            }

            bool IsValid(string? input)
            {
                if (string.IsNullOrWhiteSpace(input)) return false;
                var d = new string(input.Where(char.IsDigit).ToArray());
                if (d.Length != 14) return false;
                if (new string(d[0], 14) == d) return false;

                return d[12] == DV(d[..12], Pesos1) &&
                       d[13] == DV(d[..13], Pesos2);
            }
            ;

            char DV(string n, int[] pesos)
            {
                int soma = 0;
                for (int i = 0; i < pesos.Length; i++)
                    soma += (n[i] - '0') * pesos[i];
                int resto = soma % 11;
                int dig = resto < 2 ? 0 : 11 - resto;
                return (char)('0' + dig);
            }

        }

        public void ValidImage(Microsoft.AspNetCore.Http.IFormFile? cnhImageFormData)
        {
            if (cnhImageFormData == null)
                _notification.AddNotification("CnhImageFormData", "CNH image is required");
        }

        internal void ValidateTypeDocument(int cnhType)
        {   
            if (!Enum.IsDefined(typeof(TypeDocument), cnhType))
                _notification.AddNotification("CnhType", "CNH type invalid");
        }

        internal void ValidatorCustomerRental(CreateCustomerRentalMotorcycleDto createCustomerRentalMotorcycleDto)
        {
            if(createCustomerRentalMotorcycleDto.RentalEndDate <= DateTime.UtcNow)
                _notification.AddNotification("RentalEndDate", "Rental end date must be greater than start date");
            if(createCustomerRentalMotorcycleDto.RentalEndDate >= createCustomerRentalMotorcycleDto.ExpectedReturnDate)
                _notification.AddNotification("ExpectedReturnDate", "Expected return date must be greater than rental end date");

            var motorcycle = _motorcyclesRepositoryReadOnly.GetMotorCycleByUid(createCustomerRentalMotorcycleDto.MotorcycleUId);
            var planRental = _planRentalReadOnly.GetPlansRentalsByUId(createCustomerRentalMotorcycleDto.PlanRentalUId);
            var customer = _customerRepositoryReadOnly.GetCustomerByCnpj(createCustomerRentalMotorcycleDto.Cnpj);

            if (motorcycle is null)
                _notification.AddNotification("motorcycle", "Don't have motorcycle");
            if (planRental is null)
                _notification.AddNotification("planRental", "Don't have planRental");
            if (customer is null)
                _notification.AddNotification("customer", "Don't have customer");
            if (customer.TypeDocument != TypeDocument.A)
                _notification.AddNotification("customer", "You don't have type documento (A)");

            createCustomerRentalMotorcycleDto.SetMotorcycle(motorcycle);
            createCustomerRentalMotorcycleDto.SetPlanRental(planRental);
            createCustomerRentalMotorcycleDto.SetCustomer(customer);

        }
    }
}
