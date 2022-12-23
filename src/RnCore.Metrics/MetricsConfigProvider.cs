using Microsoft.Extensions.Configuration;
using RnCore.Metrics.Models;
using System.Reflection;
using RnCore.Metrics.Extensions;

namespace RnCore.Metrics;

public interface IMetricsConfigProvider
{
  MetricsConfig Provide();
}

public class MetricsConfigProvider : IMetricsConfigProvider
{
  private readonly MetricsConfig _config;

  public MetricsConfigProvider(IConfiguration configuration)
  {
    _config = GetRnMetricsConfig(configuration);
  }

  public MetricsConfig Provide() => _config;

  private MetricsConfig GetRnMetricsConfig(IConfiguration configuration)
  {
    MetricsConfig coreMetricsConfig = BindMetricsConfig(configuration);

    // Ensure that a valid application name is defined
    if (string.IsNullOrWhiteSpace(coreMetricsConfig.Application))
      coreMetricsConfig.Application = Assembly.GetEntryAssembly()?.GetName().Name ?? "";

    // TODO: throw better exception here
    if (string.IsNullOrWhiteSpace(coreMetricsConfig.Application))
      throw new Exception("ApplicationName is required");

    // Ensure that an environment value is defined
    if (string.IsNullOrWhiteSpace(coreMetricsConfig.Environment))
      coreMetricsConfig.Environment = "development";

    // Ensure that there is a metric template defined
    if (string.IsNullOrWhiteSpace(coreMetricsConfig.Template))
      coreMetricsConfig.Template = "{app}/{measurement}";

    // Ensure all value casing is correct
    coreMetricsConfig.Application = coreMetricsConfig.Application.LowerTrim();
    coreMetricsConfig.Environment = coreMetricsConfig.Environment.LowerTrim();

    return coreMetricsConfig;
  }

  private static MetricsConfig BindMetricsConfig(IConfiguration configuration)
  {
    var boundConfig = new MetricsConfig();

    IConfigurationSection? section = configuration.GetSection(MetricsConfig.ConfigKey);
    if (!section.Exists())
      return boundConfig;

    section.Bind(boundConfig);
    return boundConfig;
  }
}
