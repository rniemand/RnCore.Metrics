using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using RnCore.Logging;

namespace RnCore.Metrics.Extensions;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddRnCoreMetrics(this IServiceCollection services)
  {
    services.TryAddScoped(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>));

    return services
      .AddLogging(loggingBuilder =>
      {
        loggingBuilder.ClearProviders();
        loggingBuilder.SetMinimumLevel(LogLevel.Trace);
        loggingBuilder.AddSimpleConsole(consoleOptions =>
        {
          consoleOptions.SingleLine = true;
        });
      });
  }
}
