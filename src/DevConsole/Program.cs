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
  .ForService("MyService", "MyMethod")
  .WithCallCount(10)
  .WithCategory("category", "subCategory", true)
  .WithSuccess(true)
  .WithResultsCount(10);

using (builder.WithTiming())
{
  await Task.Delay(125);
}

builder.WithException(new Exception("Whoops"));

await metricsService.SubmitAsync(builder);

Console.WriteLine();
Console.WriteLine();
