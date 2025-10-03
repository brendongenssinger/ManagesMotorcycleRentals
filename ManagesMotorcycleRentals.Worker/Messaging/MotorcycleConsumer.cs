
using ManagesMotorcycleRentals.API.Messaging.Model;
using ManagesMotorcycleRentals.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace ManagesMotorcycleRentals.API.Messaging
{
    public class MotorcycleConsumer : BackgroundService
    {
        private IMotocycleServiceConsumer _motorcycleServiceConsumer;
        private readonly IServiceProvider _sp;


        public MotorcycleConsumer(IServiceProvider serviceProvider)
        {
            _sp = serviceProvider;
        }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var factory = new ConnectionFactory() { HostName = "localhost", Port = 5672, UserName = "admin", Password="admin123" };
            await using var connection = await factory.CreateConnectionAsync(stoppingToken);
            await using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(
                queue: "motorcycles",
                durable: false,
                exclusive: false,
                autoDelete: false,
                cancellationToken: stoppingToken
            );

            var consumer = new AsyncEventingBasicConsumer(channel); 

            consumer.ReceivedAsync += async (sender, ea) =>
            {
                var json = Encoding.UTF8.GetString(ea.Body.ToArray());
                var moto = JsonSerializer.Deserialize<MotorcycleMessage>(json);

                if (moto is not null && moto.Year == 2024)
                {
                    try
                    {
                        using var scope = _sp.CreateScope();
                        _motorcycleServiceConsumer = scope.ServiceProvider.GetRequiredService<IMotocycleServiceConsumer>();
                        await _motorcycleServiceConsumer.SaveMotorcycleConsumer(moto, stoppingToken);
                    }
                    catch (Exception eX)
                    {
                        throw eX;
                    }
                }


                await channel.BasicAckAsync(ea.DeliveryTag, false);

            };

            await channel.BasicConsumeAsync(queue: "motorcycles", autoAck: false, consumer: consumer);

            // 🚀 Mantém o método rodando enquanto a aplicação não for encerrada
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
