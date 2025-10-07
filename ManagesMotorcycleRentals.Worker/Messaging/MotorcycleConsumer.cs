
using ManagesMotorcycleRentals.API.Messaging.Model;
using ManagesMotorcycleRentals.Application.Services.Interfaces;
using MassTransit;

namespace ManagesMotorcycleRentals.API.Messaging
{
    public class MotorcycleConsumer : IConsumer<MotorcycleMessage>
    {
        private IMotocycleServiceConsumer _motorcycleServiceConsumer;



        public MotorcycleConsumer(IMotocycleServiceConsumer motocycleServiceConsumer)
        {
            _motorcycleServiceConsumer = motocycleServiceConsumer;
        }

        public async Task Consume(ConsumeContext<MotorcycleMessage> context)
        {
            var moto = context.Message;

            if (moto is not null && moto.Year == 2024)
            {
                try
                {
                    await _motorcycleServiceConsumer.SaveMotorcycleConsumer(moto, context.CancellationToken);
                }
                catch (Exception eX)
                {
                    throw eX;
                }
            }
        }

        public class MotorcycleConsumerRegister : ConsumerDefinition<MotorcycleConsumer>
        {
            protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<MotorcycleConsumer> consumerConfigurator)
            {
                consumerConfigurator.ConcurrentMessageLimit = 1;
                consumerConfigurator.UseMessageRetry(x => x.Intervals(10, 100, 500, 1000));
            }

        }
    }
}
