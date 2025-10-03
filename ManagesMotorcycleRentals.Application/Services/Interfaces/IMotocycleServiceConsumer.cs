using ManagesMotorcycleRentals.API.Messaging.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagesMotorcycleRentals.Application.Services.Interfaces
{
    public interface IMotocycleServiceConsumer
    {
        Task SaveMotorcycleConsumer(MotorcycleMessage motorcycleMessage, CancellationToken cancellation);
    }
}
