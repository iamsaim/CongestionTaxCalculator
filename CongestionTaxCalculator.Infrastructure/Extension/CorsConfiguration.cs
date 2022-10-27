using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CongestionTaxCalculator.Infrastructure.Extension
{
    public static class CorsConfiguration
    {
        public static IServiceCollection ConfigureCorsPolicy(this IServiceCollection services,
            IConfiguration configuration, string policyName)
        {
            services.AddCors(config =>
                config.AddPolicy(policyName,
                    p => p.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()));

            return services;
        }
    }
}