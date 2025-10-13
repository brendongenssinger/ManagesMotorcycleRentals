using ManagesMotorcycleRentals.API.Messaging.Model;
using ManagesMotorcycleRentals.Application.DTOs;
using MassTransit;

namespace ManagesMotorcycleRentals.Worker.Event
{
    public class MotorcycleCreatedEvent : IConsumer<MotorcycleCreatedEventDto>
    {
        public async Task Consume(ConsumeContext<MotorcycleCreatedEventDto> context)
        {
            Console.WriteLine($"MotorcycleCreatedEvent received: {context.Message.LicensePlate}, {context.Message.Model}, {context.Message.Year}"); 

            await context.Send(new MotorcycleMessage()
            {
                LicensePlate = context.Message.LicensePlate,
                Model = context.Message.Model,
                Year = context.Message.Year,
            }, context.CancellationToken);

        }
    }
}
