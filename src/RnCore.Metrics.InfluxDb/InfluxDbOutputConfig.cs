using Microsoft.Extensions.Configuration;

namespace RnCore.Metrics.InfluxDb;

public class InfluxDbOutputConfig
{
  public const string ConfigKey = "RnCore.Metrics.InfluxDb";

  [ConfigurationKeyName("token")]
  public string Token { get; set; } = string.Empty;

  [ConfigurationKeyName("bucket")]
  public string Bucket { get; set; } = string.Empty;

  [ConfigurationKeyName("org")]
  public string Org { get; set; } = string.Empty;

  [ConfigurationKeyName("url")]
  public string Url { get; set; } = string.Empty;

  [ConfigurationKeyName("enabled")]
  public bool Enabled { get; set; }
}
