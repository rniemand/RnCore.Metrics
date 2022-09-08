using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Writes;
using RnCore.Metrics.Models;
using RnCore.Metrics.Outputs;

namespace RnCore.Metrics.InfluxDb;

public class InfluxDbOutput : IMetricOutput
{
  public bool Enabled { get; }

  public string Name => nameof(InfluxDbOutput);

  private readonly InfluxDbOutputConfig _config;

  public InfluxDbOutput(IInfluxDbOutputConfigProvider configProvider)
  {
    _config = configProvider.GetConfig();
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
      writeApi.WritePoint(BuildPointData(metric), _config.Bucket, _config.Org);

    await Task.CompletedTask;
  }


  // Internal methods
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
