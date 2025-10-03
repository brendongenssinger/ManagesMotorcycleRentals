using ManagesMotorcycleRentals.API.Messaging;
using ManagesMotorcycleRentals.Application.Services.Interfaces;
using ManagesMotorcycleRentals.Application.Services.Motorcycles;
using ManagesMotorcycleRentals.Infrastructure.Context;
using ManagesMotorcycleRentals.Infrastructure.Interfaces;
using ManagesMotorcycleRentals.Infrastructure.Repositories;
using ManagesMotorcycleRentals.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = Host.CreateApplicationBuilder(args);
// Consumer 
builder.Services.AddScoped<IMotocyclesRepository, MotorcyclesRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IMotocycleServiceConsumer, MotocycleServiceConsumer>();
builder.Services.AddHostedService<MotorcycleConsumer>();

//Connection String
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//Context
builder.Services.AddDbContext<ManagesMotorcyclesRentalsDbContext>(act =>
{
    act.UseNpgsql(connectionString);
});

var host = builder.Build();
host.Run();
