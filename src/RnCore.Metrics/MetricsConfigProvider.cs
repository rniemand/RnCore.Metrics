using Microsoft.Extensions.Configuration;
using RnCore.Metrics.Models;
using System.Reflection;
using RnCore.Metrics.Extensions;

namespace RnCore.Metrics;

public interface IMetricsConfigProvider
{
  RnCoreMetricsConfig Provide();
}

public class MetricsConfigProvider : IMetricsConfigProvider
{
  private readonly RnCoreMetricsConfig _config;

  public MetricsConfigProvider(IConfiguration configuration)
  {
    _config = GetRnMetricsConfig(configuration);
  }

  public RnCoreMetricsConfig Provide() => _config;

  private RnCoreMetricsConfig GetRnMetricsConfig(IConfiguration configuration)
  {
    RnCoreMetricsConfig coreMetricsConfig = BindMetricsConfig(configuration);

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

  private static RnCoreMetricsConfig BindMetricsConfig(IConfiguration configuration)
  {
    var boundConfig = new RnCoreMetricsConfig();

    IConfigurationSection? section = configuration.GetSection(RnCoreMetricsConfig.ConfigKey);
    if (!section.Exists())
      return boundConfig;

    section.Bind(boundConfig);
    return boundConfig;
  }
}
