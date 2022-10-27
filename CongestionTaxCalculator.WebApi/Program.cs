using CongestionTaxCalculator.Infrastructure.Extension;
using Serilog;

namespace CongestionTaxCalculator.WebApi
{
    public class Program
    {
        public static bool IsStartedWithMain { get; private set; }
        public static void Main(string[] args)
        {
            IsStartedWithMain = true;

            // Creating Temporary bootstrap logger
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            try
            {
                Log.Information("Starting web host");
                
                // Creating web host
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            ConfigureWebHost.CreateDefaultHostBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
