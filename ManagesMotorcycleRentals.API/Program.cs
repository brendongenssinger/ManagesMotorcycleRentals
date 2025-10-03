using Asp.Versioning;
using ManagesMotorcycleRentals.API.Configuration;
using ManagesMotorcycleRentals.API.Messaging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApiConfiguration();

builder.Services.AddDependencyInjectionConfiguration(builder.Configuration);
builder.Services.AddJwtConfiguration(builder.Configuration);
builder.Services.AddAmazonS3Configuration();

builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
});
var app = builder.Build();

app.UseApiConfiguration();

app.UseAuthentication();
app.UseAuthorization();

app.Run();
