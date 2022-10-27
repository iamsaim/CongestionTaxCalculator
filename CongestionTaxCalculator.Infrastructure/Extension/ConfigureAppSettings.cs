using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CongestionTaxCalculator.Infrastructure.Extension
{
    public static class ConfigureAppSettings
    {
        public static IServiceCollection RegisterAppSettings(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            // TODO - Add Configuration settings
            return serviceCollection;
        }
    }
}
