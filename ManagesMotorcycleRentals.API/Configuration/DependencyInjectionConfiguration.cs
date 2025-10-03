using ManagesMotorcycleRentals.Application.Services.Interfaces;
using ManagesMotorcycleRentals.Application.Services.Motorcycles;
using ManagesMotorcycleRentals.Application.Services.User;
using ManagesMotorcycleRentals.Application.Services.Validator;
using ManagesMotorcycleRentals.Domain.Shared;
using ManagesMotorcycleRentals.Infrastructure.Context;
using ManagesMotorcycleRentals.Infrastructure.Interfaces;
using ManagesMotorcycleRentals.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

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
            services.AddScoped<IUserService, UserService>();

            // Repository Read Only
            services.AddScoped<IMotorcyclesRepositoryReadOnly, MotorcyclesRepositoryReadOnly>();

            // Repository 
            services.AddScoped<IMotocyclesRepository, MotorcyclesRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Validator Service
            services.AddScoped<MotorcyclesServicesValidator>();
            services.AddScoped<UserServiceValidator>();

            //shared
            services.AddScoped<Notifiable>();

            return services;
        }
    }
}
