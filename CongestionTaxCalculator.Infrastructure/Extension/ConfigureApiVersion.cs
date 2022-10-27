using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;

namespace CongestionTaxCalculator.Infrastructure.Extension
{
    public static class ConfigureApiVersion
    {
        public static IServiceCollection ConfigureVersion(this IServiceCollection serviceCollection, ApiVersion version, bool assumeDefaultVersionWhenUnspecified)
        {
            return serviceCollection.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = version;
                config.AssumeDefaultVersionWhenUnspecified = assumeDefaultVersionWhenUnspecified;
                config.ReportApiVersions = true;
                config.ApiVersionReader = new UrlSegmentApiVersionReader();
            }).AddVersionedApiExplorer(options =>
            {
                options.SubstituteApiVersionInUrl = true;
            });
        }
    }
}
