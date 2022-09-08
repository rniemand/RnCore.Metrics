using DevConsole.Builders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RnCore.Metrics;
using RnCore.Metrics.Extensions;
using RnCore.Metrics.InfluxDb;
using RnCore.Metrics.Outputs;

IConfigurationRoot config = new ConfigurationBuilder()
  .AddJsonFile("appsettings.json")
  .AddJsonFile("appsettings.machine.json", optional: true)
  .Build();

ServiceProvider serviceProvider = new ServiceCollection()
  .AddSingleton<IConfiguration>(config)
  .AddRnCoreMetrics()
  .AddSingleton<IMetricOutput, InfluxDbOutput>()
  .BuildServiceProvider();

var metricsService = serviceProvider.GetRequiredService<IMetricsService>();

CustomMetricBuilder builder = new CustomMetricBuilder()
  .WithSuccess(true);

using (builder.WithTiming())
{
  await Task.Delay(125);
}

builder.WithException(new Exception("Whoops"));

await metricsService.SubmitAsync(builder);

Console.WriteLine();
Console.WriteLine();
