using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RnCore.Metrics;
using RnCore.Metrics.Builders;
using RnCore.Metrics.Extensions;

IConfigurationRoot config = new ConfigurationBuilder()
  .AddJsonFile("appsettings.json")
  .Build();

ServiceProvider serviceProvider = new ServiceCollection()
  .AddSingleton<IConfiguration>(config)
  .AddRnCoreMetrics()
  .BuildServiceProvider();

var metricsService = serviceProvider.GetRequiredService<IMetricsService>();

ServiceMetricBuilder builder = new ServiceMetricBuilder()
  .ForService("MyService", "MyMethod");

using (builder.WithTiming())
{
  await Task.Delay(125);
}

await metricsService.SubmitAsync(builder);

Console.WriteLine();
Console.WriteLine();
