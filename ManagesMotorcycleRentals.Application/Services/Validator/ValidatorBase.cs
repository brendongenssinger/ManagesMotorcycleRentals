using ManagesMotorcycleRentals.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagesMotorcycleRentals.Application.Services.Validator
{
    public abstract class ValidatorBase
    {
        public  readonly Notify _notification;

        protected ValidatorBase(Notify notify)
        {
            _notification = notify;
        }
    }
}
