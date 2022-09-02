using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RnCore.Logging;
using RnCore.Metrics.Extensions;

ServiceProvider serviceProvider = new ServiceCollection()
  .AddSingleton(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build())
  .AddRnCoreMetrics()
  .BuildServiceProvider();

serviceProvider
  .GetRequiredService<ILoggerAdapter<Program>>()
  .LogInformation("Hello World");

