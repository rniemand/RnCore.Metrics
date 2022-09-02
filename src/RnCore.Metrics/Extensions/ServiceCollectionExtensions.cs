using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using RnCore.Logging;
using RnCore.Metrics.Abstractions;
using RnCore.Metrics.Outputs;

namespace RnCore.Metrics.Extensions;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddRnCoreMetrics(this IServiceCollection services, bool useDefaultConfigProvider = true)
  {
    services.TryAddScoped(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>));

    if (useDefaultConfigProvider)
      services.AddSingleton<IRnMetricsConfigProvider, RnMetricsConfigProvider>();

    return services
      .AddSingleton<IMetricsService, MetricsService>()
      .AddSingleton<IDateTimeAbstraction, DateTimeAbstraction>()
      .AddSingleton<IMetricOutput, ConsoleMetricOutput>()

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
