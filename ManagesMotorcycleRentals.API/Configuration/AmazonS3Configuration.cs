using Amazon.S3;

namespace ManagesMotorcycleRentals.API.Configuration
{
    public static class AmazonS3Configuration
    {
        public static IServiceCollection AddAmazonS3Configuration(this IServiceCollection services)
        {
            services.AddSingleton<IAmazonS3>(sp =>
            {
                var config = new AmazonS3Config
                {
                    ServiceURL = "http://localhost:9000", 
                    ForcePathStyle = true                 
                };

                return new AmazonS3Client("admin", "admin123", config);
            });
            return services;
        }
    }
}
