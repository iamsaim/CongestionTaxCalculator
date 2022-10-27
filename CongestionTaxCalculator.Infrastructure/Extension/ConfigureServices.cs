using CongestionTaxCalculator.Application.Common.Contracts;
using CongestionTaxCalculator.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CongestionTaxCalculator.Infrastructure.Extension
{
    public static class ConfigureServices
    {
        public static IServiceCollection ConfigureService(this IServiceCollection services)
        {
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<ICongestionTaxService, CongestionTaxService>();
            services.AddScoped<ITaxExemptVehicleService, TaxExemptVehicleService>();
            services.AddScoped<ITaxFreeDayService, TaxFreeDayService>();
            services.AddScoped<ITaxScheduleService, TaxScheduleService>();
            services.AddScoped<IVehicleService, VehicleService>();

            return services;
        }
    }
}
