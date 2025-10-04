using ManagesMotorcycleRentals.Domain.Shared;
using Microsoft.AspNetCore.Mvc;

namespace ManagesMotorcycleRentals.API.Controllers
{
    public class ControllerBase : Controller
    {
        private Notify _notify;

        public ControllerBase(Notify notify)
        {
            _notify = notify;
        }

        [NonAction]
        public IActionResult ResponseData(object obj)
        {
            if (_notify.HasNotifications)
            {
                return StatusCode(_notify.StatusCode, new
                {
                    success = false,
                    data = _notify.Notifications
                });
            }
            else
            {
                return Ok(new
                {
                    success = true,
                    data = obj
                });
            }
        }
    }
}
