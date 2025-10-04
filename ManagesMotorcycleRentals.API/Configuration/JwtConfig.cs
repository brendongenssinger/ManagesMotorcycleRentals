using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace ManagesMotorcycleRentals.API.Configuration
{
    public static class JwtConfig
    {
        public static void AddJwtConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            const string hash = "b7bb4e29f59e62c716a9f17986d1a861bf8b4689a0233ef31b0e77d5a78f4935";
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(hash));

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = "https://api-brendon.com",
                    ValidAudience = "http://teste-brendon.api",
                };

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine("Token inválido: " + context.Exception.Message);
                        return Task.CompletedTask;
                    },
                    OnForbidden = context =>
                    {
                        Console.WriteLine("Token forbidden: ");
                        return Task.CompletedTask;
                    },
                    OnMessageReceived = context =>
                    {   
                        return Task.CompletedTask;
                    }
                    
                };
            });
        }
    }
}
