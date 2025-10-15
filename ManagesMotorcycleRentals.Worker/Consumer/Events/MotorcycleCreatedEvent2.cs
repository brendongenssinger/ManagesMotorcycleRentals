using ManagesMotorcycleRentals.Application.DTOs;
using MassTransit;

namespace ManagesMotorcycleRentals.Worker.Consumer.Events
{
    public class MotorcycleCreatedEvent2 : IConsumer<MotorcycleCreatedEventDto>
    {
        public async Task Consume(ConsumeContext<MotorcycleCreatedEventDto> context)
        {
            Console.WriteLine($"[Event 2] == MotorcycleCreatedEvent2 received: {context.Message.LicensePlate}, {context.Message.Model}, {context.Message.Year}"); 
        }
    }
}
