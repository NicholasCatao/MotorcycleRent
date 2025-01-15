using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RentMotorBike.Workers
{
    public static class SetupInjection
    {
        public static IServiceCollection AddDi(IServiceCollection service, IConfiguration configuration)
        {
            var namedClients = new Dictionary<string, string>
            {
                {"", ""}
            };

            var retryPolicy = HttpPolicyExtensions
                .HandleTransientHttpError() // HttpRequestException, 5XX and 408
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(retryAttempt));


            foreach (var (namedClient, baseAddress) in namedClients)
            {
                service.AddHttpClient(namedClient, cfg =>
                {
                    cfg.BaseAddress = new Uri(baseAddress);
                });
            }

            return service;
        }
    }



}
