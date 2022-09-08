using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Writes;
using Microsoft.Extensions.Configuration;
using RnCore.Metrics.Models;
using RnCore.Metrics.Outputs;

namespace DevConsole;

public class InfluxDbOutputConfig
{
  public const string ConfigKey = "Rn.Metrics.InfluxDb";

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

public class InfluxDbOutput : IMetricOutput
{
  public bool Enabled { get; }

  public string Name => nameof(InfluxDbOutput);

  private readonly InfluxDbOutputConfig _config;

  public InfluxDbOutput(IConfiguration configuration)
  {
    _config = GetConfiguration(configuration);
    Enabled = _config.Enabled;
  }

  public async Task SubmitMetric(RnMetric metric)
  {
    if (!Enabled)
      return;

    await SubmitMetrics(new List<RnMetric> { metric });
  }

  public async Task SubmitMetrics(List<RnMetric> metrics)
  {
    if (!Enabled)
      return;

    using var client = GetClient();
    using var writeApi = client.GetWriteApi();
    foreach (RnMetric metric in metrics)
    {
      writeApi.WritePoint(BuildPointData(metric), _config.Bucket, _config.Org);
    }

    await Task.CompletedTask;
  }


  // Internal methods
  private static InfluxDbOutputConfig GetConfiguration(IConfiguration configuration)
  {
    var config = new InfluxDbOutputConfig();

    var section = configuration.GetSection(InfluxDbOutputConfig.ConfigKey);
    if (!section.Exists())
      return config;

    section.Bind(config);
    return config;
  }

  private IInfluxDBClient GetClient() =>
    InfluxDBClientFactory.Create(_config.Url, _config.Token);

  private static PointData BuildPointData(RnMetric metric)
  {
    var pointData = PointData
      .Measurement(metric.Measurement)
      .Timestamp(metric.UtcTimestamp, WritePrecision.Ns);

    foreach (var (key, value) in metric.Fields)
      pointData = pointData.Field(key, value);

    foreach (var (key, value) in metric.Tags)
      pointData = pointData.Tag(key, value);

    return pointData;
  }
}
