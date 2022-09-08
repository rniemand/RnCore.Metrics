using Microsoft.Extensions.DependencyInjection;
using RnCore.Metrics.Outputs;

namespace RnCore.Metrics.InfluxDb;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddInfluxDbMetricOutput(this IServiceCollection services, bool useDefaultConfigProvider = true)
  {
    if (useDefaultConfigProvider)
      services.AddSingleton<IInfluxDbOutputConfigProvider, InfluxDbOutputConfigProvider>();

    return services
      .AddSingleton<IMetricOutput, InfluxDbMetricOutput>();
  }
}
