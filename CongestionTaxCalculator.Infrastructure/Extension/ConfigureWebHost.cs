using Microsoft.Extensions.Hosting;

namespace CongestionTaxCalculator.Infrastructure.Extension
{
    public static class ConfigureWebHost
    {
        public static IHostBuilder CreateDefaultHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .AddLogging();
        }
            
    }
}