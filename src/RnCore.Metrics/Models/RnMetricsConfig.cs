using Microsoft.Extensions.Configuration;

namespace RnCore.Metrics.Models;

public class RnMetricsConfig
{
  public const string ConfigKey = "Rn.Metrics";

  [ConfigurationKeyName("enabled")]
  public bool Enabled { get; set; }

  [ConfigurationKeyName("application")]
  public string Application { get; set; } = string.Empty;

  [ConfigurationKeyName("template")]
  public string Template { get; set; } = "{app}/{measurement}";

  [ConfigurationKeyName("overrides")]
  public Dictionary<string, string> Overrides { get; set; } = new();

  [ConfigurationKeyName("environment")]
  public string Environment { get; set; } = "production";

  [ConfigurationKeyName("enableConsoleOutput")]
  public bool EnableConsoleOutput { get; set; } = false;
}
