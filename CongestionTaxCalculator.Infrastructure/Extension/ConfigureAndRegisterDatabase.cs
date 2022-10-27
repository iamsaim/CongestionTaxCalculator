using CongestionTaxCalculator.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CongestionTaxCalculator.Infrastructure.Extension
{
    public static class ConfigureAndRegisterDatabase
    {
        public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            // Configuring identity Db context
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("TaxCalculator"));

            return services;
        }
    }
}
