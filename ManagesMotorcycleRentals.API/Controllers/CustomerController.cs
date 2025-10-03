using Asp.Versioning;
using ManagesMotorcycleRentals.Domain.Shared;
using Microsoft.AspNetCore.Mvc;

namespace ManagesMotorcycleRentals.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}")]
    public class CustomerController : ControllerBase
    {
        public CustomerController(Notifiable notifiable) : base(notifiable)
        {

        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomerAsync([FromBody] )
    }
}
