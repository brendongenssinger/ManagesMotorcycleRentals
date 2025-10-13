using ManagesMotorcycleRentals.API.Messaging;
using ManagesMotorcycleRentals.Application.Services.Interfaces;
using ManagesMotorcycleRentals.Application.Services.Motorcycles;
using ManagesMotorcycleRentals.Infrastructure.Context;
using ManagesMotorcycleRentals.Infrastructure.Interfaces;
using ManagesMotorcycleRentals.Infrastructure.Repositories;
using ManagesMotorcycleRentals.Worker.Event;
using MassTransit;
using MassTransit.Transports.Fabric;
using Microsoft.EntityFrameworkCore;
var builder = Host.CreateApplicationBuilder(args);
    
// Consumer 
builder.Services.AddScoped<IMotocyclesRepository, MotorcyclesRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IMotorcyclesAllocationsRepository, MotorcyclesAllocationsRepository>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IMotocycleServiceConsumer, MotocycleServiceConsumer>();

builder.Services.Configure<MassTransitHostOptions>(options =>
{
    options.WaitUntilStarted = true;
    options.StartTimeout = TimeSpan.FromSeconds(30);
    options.StopTimeout = TimeSpan.FromMinutes(1);
});

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<MotorcycleConsumer>();

   

    x.UsingRabbitMq((context, cfg) =>
    {
        //cfg.Host("rabbitmq", h =>
        cfg.Host(builder.Configuration.GetSection("RabbitMq:Host").Value, h =>
        {
            h.Username(builder.Configuration.GetSection("RabbitMq:User").Value);
            h.Password(builder.Configuration.GetSection("RabbitMq:Password").Value);            
        });
        
        cfg.ReceiveEndpoint("motorcycles", e =>
        {
            e.UseConcurrencyLimit(1);
            e.UseRateLimit(50, TimeSpan.FromMinutes(1));            
            e.UseKillSwitch(options => options
                .SetActivationThreshold(10)
                .SetTripThreshold(0.15)
                .SetRestartTimeout(m: 1)
            );

            e.ConfigureConsumer<MotorcycleConsumer>(context);
        });

        cfg.Message<MotorcycleCreatedEvent>(m =>
        {
            m.SetEntityName("moto.delivery.topic");
        });
    });
});


//Connection String
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//Context
builder.Services.AddDbContext<ManagesMotorcyclesRentalsDbContext>(act =>
{
    act.UseNpgsql(connectionString);
});

var host = builder.Build();
host.Run();
