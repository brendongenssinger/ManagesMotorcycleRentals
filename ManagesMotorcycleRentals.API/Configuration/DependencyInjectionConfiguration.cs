using ManagesMotorcycleRentals.Application.DTOs;
using ManagesMotorcycleRentals.Application.Services.Customer;
using ManagesMotorcycleRentals.Application.Services.Interfaces;
using ManagesMotorcycleRentals.Application.Services.Motorcycles;
using ManagesMotorcycleRentals.Application.Services.PlanRentals;
using ManagesMotorcycleRentals.Application.Services.User;
using ManagesMotorcycleRentals.Application.Services.Validator;
using ManagesMotorcycleRentals.Domain.Entities;
using ManagesMotorcycleRentals.Domain.Shared;
using ManagesMotorcycleRentals.Infrastructure.Context;
using ManagesMotorcycleRentals.Infrastructure.Interfaces;
using ManagesMotorcycleRentals.Infrastructure.Repositories;
using MassTransit;
using MassTransit.Transports.Fabric;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace ManagesMotorcycleRentals.API.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection AddDependencyInjectionConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            //Context
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ManagesMotorcyclesRentalsDbContext>(act =>
            {
                act.UseNpgsql(connectionString);
                
            });

            // Services
            services.AddScoped<IMotorcyclesServices, MotorcyclesServices>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPlanRentalsService, PlanRentalsService>();
            

            // Repository Read Only
            services.AddScoped<IMotorcyclesRepositoryReadOnly, MotorcyclesRepositoryReadOnly>();
            services.AddScoped<ICustomerRepositoryReadOnly, CustomerRepositoryReadOnly>();
            services.AddScoped<IPlanRentaRepositorylReadOnly, PlanRentaRepositorylReadOnly>();


            // Repository
            services.AddScoped<IMotocyclesRepository, MotorcyclesRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IMotorcyclesAllocationsRepository, MotorcyclesAllocationsRepository>();

            //Unit of Work
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Validator Service
            services.AddScoped<MotorcyclesServicesValidator>();
            services.AddScoped<CustomerServiceValidator>();
            services.AddScoped<UserServiceValidator>();

            //shared
            services.AddScoped<Notify>();

            //rabbitMQ
            services.AddMassTransit(x =>
            {
                x.SetKebabCaseEndpointNameFormatter();
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(configuration.GetSection("RabbitMQ:Host").Value ?? string.Empty, h =>                    
                    {
                        h.Username(configuration.GetSection("RabbitMQ:User").Value ?? string.Empty);
                        h.Password(configuration.GetSection("RabbitMQ:Password").Value ?? string.Empty);                        
                    });               
                   
                });

            });

            return services;
        }
    }
}
