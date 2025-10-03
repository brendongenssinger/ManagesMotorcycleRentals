using ManagesMotorcycleRentals.Application.Services.Interfaces;
using ManagesMotorcycleRentals.Domain.Shared;
using ManagesMotorcycleRentals.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;

namespace ManagesMotorcycleRentals.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}")]
    public class MotorCyclesController : ControllerBase
    {
        private IMotorcyclesServices _motorcyclesServices;

        public MotorCyclesController(Notifiable notifiable, IMotorcyclesServices motorcyclesServices) : base(notifiable)
        {
            _motorcyclesServices = motorcyclesServices;
        }

       [Authorize(AuthenticationSchemes = "Bearer", Roles = "admin")]
       [HttpPost("motorcycle/create")]
       public async Task<IActionResult> CreateMotorCycle([FromBody] MotorCycleCreateDto motorCycleCreateDto, CancellationToken cancellationToken)
       {
            var result = await _motorcyclesServices.CreateMotorcycleAsync(motorCycleCreateDto, cancellationToken);

            return ResponseData(result);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "admin")]
        [HttpGet("motorcycle/list/licence-plate")]
        public async Task<IActionResult> GetMotorCycleByLicensePlate([FromQuery] string licensePlate, CancellationToken cancellationToken)
        {
            var result = await _motorcyclesServices.GetMotorcyles(licensePlate, cancellationToken);

            return ResponseData(result);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "admin")]
        [HttpPatch("motorcycle/update/licence-plate")]
        public async Task<IActionResult> UpdateMotorCycleByLicensePlate([FromBody] MotorCycleUpdateDto motorCycleUpdateDto, CancellationToken cancellationToken)
        {
            var result = await _motorcyclesServices.UpdateMotorcycle(motorCycleUpdateDto, cancellationToken);

            return ResponseData(result);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "admin")]
        [HttpDelete("motorcycle/delete/{motorcycleByUId}")]
        public async Task<IActionResult> DeleteMotorCycleByUId(Guid motorcycleByUId, CancellationToken cancellationToken)
        {
            var result = await _motorcyclesServices.DeleteMotorcycleByUId(motorcycleByUId, cancellationToken);

            return ResponseData(result);
        }
    }
}
