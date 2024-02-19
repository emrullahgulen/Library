using Serilog;
using Serilog.Filters;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Hosting;
using Serilog.Events;

namespace Utilities.Logger
{
    public static class SeriLoggingExtensions
    {
        public static IHostBuilder UseSeriLogging(this IHostBuilder builder)
        {
            var file = "Full_.log";
            var folderName = Path.Combine("tmpfiles");
            var logfolderName = Path.Combine(folderName, "logs");
            if (!System.IO.Directory.Exists(logfolderName))
                System.IO.Directory.CreateDirectory(logfolderName);
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), logfolderName);
            var fulllogfilename = Path.Combine(pathToSave, file);
            Console.WriteLine($"API Log Path:{fulllogfilename}");

            var logLevel = LogEventLevel.Information;
            var logFileSize = 1024000 ;

            var hostBuilder = builder.UseSerilog((context, loggerConfiguration) =>
            {
                loggerConfiguration
                    .MinimumLevel.Is(logLevel)
                    .Enrich.FromLogContext()
                    .WriteTo.Console()
                    .WriteTo.Debug()
                    .Filter.ByExcluding(Matching.WithProperty("RequestPath", "/health"))
                    .WriteTo.File(fulllogfilename, rollingInterval: RollingInterval.Day, restrictedToMinimumLevel: logLevel, rollOnFileSizeLimit:true, fileSizeLimitBytes: logFileSize);
            });
            return hostBuilder;
        }

       }
}