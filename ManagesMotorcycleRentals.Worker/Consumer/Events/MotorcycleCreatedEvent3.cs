using ManagesMotorcycleRentals.Application.DTOs;
using MassTransit;

namespace ManagesMotorcycleRentals.Worker.Consumer.Events
{
    public class MotorcycleCreatedEvent3 : IConsumer<MotorcycleCreatedEventDto>
    {
        public async Task Consume(ConsumeContext<MotorcycleCreatedEventDto> context)
        {
            Console.WriteLine($"[Event 3] == MotorcycleCreatedEvent2 received: {context.Message.LicensePlate}, {context.Message.Model}, {context.Message.Year}");
        }
    }
}
