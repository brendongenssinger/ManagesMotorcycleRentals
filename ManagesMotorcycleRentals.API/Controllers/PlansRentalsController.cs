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
    public class PlansRentalsController : ControllerBase
    {
        public readonly IPlanRentalsService _plansRentalsService;
        public PlansRentalsController(Notify notify, IPlanRentalsService planRentalsService) : base(notify)
        {
            _plansRentalsService = planRentalsService;
        }


        [Authorize(AuthenticationSchemes = "Bearer", Roles = "admin,user")]
        [HttpGet("plansrentals/all")]
        public async Task<IActionResult> CreateCustomerAsync(CancellationToken cancellationToken)
        {
            return ResponseData(await _plansRentalsService.GetPlansRentalsAsync(cancellationToken));
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "admin,user")]
        [HttpGet("plansrentals/{plansRentalUId}")]
        public async Task<IActionResult> CreateCustomerAsync(Guid plansRentalUId, CancellationToken cancellationToken)
        {
            return ResponseData(await _plansRentalsService.GetPlansRentalsByUIdAsync(plansRentalUId, cancellationToken));
        }



    }
}
