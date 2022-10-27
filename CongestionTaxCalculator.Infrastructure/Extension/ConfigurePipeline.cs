using CongestionTaxCalculator.Application.Common.Middleware;
using Microsoft.AspNetCore.Builder;

namespace CongestionTaxCalculator.Infrastructure.Extension
{
    public static class ConfigurePipeline
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<UnhandledExceptionMiddleware>();
        }
    }
}