using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace Acme.Ecommerce.DbMigrator;

class Program
{
    static async Task Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("Volo.Abp", LogEventLevel.Warning)
#if DEBUG
                .MinimumLevel.Override("Acme.Ecommerce", LogEventLevel.Debug)
#else
                .MinimumLevel.Override("Acme.Ecommerce", LogEventLevel.Information)
#endif
                .Enrich.FromLogContext()
            .WriteTo.Async(c => c.File("Logs/logs.txt"))
            .WriteTo.Async(c => c.Console())
            .CreateLogger();

        try
        {
            Log.Information("Starting database migration...");
            await CreateHostBuilder(args).RunConsoleAsync();
        }
        catch (Exception ex)
        {
            // This block will execute when the application crashes
            Log.Fatal(ex, "Database migration failed!");
            Console.WriteLine("----------- FATAL ERROR -----------");
            Console.WriteLine(ex.ToString()); // This prints the full exception with all inner exceptions
        }
        finally
        {
            // This block will always execute, keeping the window open
            Log.CloseAndFlush();
            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();
        }
    }

    // This method was missing from my previous answer, causing the error.
    // It needs to be here.
    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .AddAppSettingsSecretsJson()
            .ConfigureLogging((context, logging) => logging.ClearProviders())
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<DbMigratorHostedService>();
            });
}