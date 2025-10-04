using Asp.Versioning;
using ManagesMotorcycleRentals.Application.DTOs;
using ManagesMotorcycleRentals.Application.Services.Interfaces;
using ManagesMotorcycleRentals.Domain.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManagesMotorcycleRentals.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}")]
    public class CustomerController : ControllerBase
    {
        public readonly ICustomerService _customerService;
        public CustomerController(Notify notification, ICustomerService customerService) : base(notification)
        {
            _customerService = customerService;
        }


        [Authorize(AuthenticationSchemes = "Bearer", Roles = "user")]
        [HttpPost("customer/create")]
        public async Task<IActionResult> CreateCustomerAsync([FromForm] CreateCustomerDto createCustomerDto, CancellationToken cancellationToken)
        {
            return ResponseData(await _customerService.CreateCustomerAsync(createCustomerDto, cancellationToken));
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "user")]
        [HttpPost("customer/create-rental")]
        public async Task<IActionResult> CreateRentalAsync([FromBody] CreateCustomerRentalMotorcycleDto createCustomerRentalMotorcycleDto, CancellationToken cancellationToken)
        {
            return ResponseData(await _customerService.CreateCustomerRentalMotorcycleAsync(createCustomerRentalMotorcycleDto,cancellationToken));
        }

        

    }
}
