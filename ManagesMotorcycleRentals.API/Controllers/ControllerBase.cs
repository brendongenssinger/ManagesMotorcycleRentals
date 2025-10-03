using ManagesMotorcycleRentals.Domain.Shared;
using Microsoft.AspNetCore.Mvc;

namespace ManagesMotorcycleRentals.API.Controllers
{
    public class ControllerBase : Controller
    {
        private Notifiable _notifiable;

        public ControllerBase(Notifiable notifiable)
        {
            _notifiable = notifiable;
        }

        [NonAction]
        public IActionResult ResponseData(object obj)
        {
            if (_notifiable.HasNotifications)
            {
                return StatusCode(_notifiable.StatusCode, new
                {
                    success = false,
                    data = _notifiable.Notifications
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
