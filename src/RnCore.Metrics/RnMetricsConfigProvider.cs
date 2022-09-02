using Microsoft.Extensions.Configuration;
using RnCore.Metrics.Models;
using System.Reflection;
using RnCore.Metrics.Extensions;

namespace RnCore.Metrics;

public interface IRnMetricsConfigProvider
{
  RnMetricsConfig Provide();
}

public class RnMetricsConfigProvider : IRnMetricsConfigProvider
{
  private readonly RnMetricsConfig _config;

  public RnMetricsConfigProvider(IConfiguration configuration)
  {
    _config = GetRnMetricsConfig(configuration);
  }

  public RnMetricsConfig Provide() => _config;

  private RnMetricsConfig GetRnMetricsConfig(IConfiguration configuration)
  {
    RnMetricsConfig metricsConfig = BindMetricsConfig(configuration);

    // Ensure that a valid application name is defined
    if (string.IsNullOrWhiteSpace(metricsConfig.Application))
      metricsConfig.Application = Assembly.GetEntryAssembly()?.GetName().Name ?? "";

    // TODO: throw better exception here
    if (string.IsNullOrWhiteSpace(metricsConfig.Application))
      throw new Exception("ApplicationName is required");

    // Ensure that an environment value is defined
    if (string.IsNullOrWhiteSpace(metricsConfig.Environment))
      metricsConfig.Environment = "development";

    // Ensure that there is a metric template defined
    if (string.IsNullOrWhiteSpace(metricsConfig.Template))
      metricsConfig.Template = "{app}/{measurement}";

    // Ensure all value casing is correct
    metricsConfig.Application = metricsConfig.Application.LowerTrim();
    metricsConfig.Environment = metricsConfig.Environment.LowerTrim();

    return metricsConfig;
  }

  private static RnMetricsConfig BindMetricsConfig(IConfiguration configuration)
  {
    var boundConfig = new RnMetricsConfig();

    IConfigurationSection? section = configuration.GetSection(RnMetricsConfig.ConfigKey);
    if (!section.Exists())
      return boundConfig;

    section.Bind(boundConfig);
    return boundConfig;
  }
}
