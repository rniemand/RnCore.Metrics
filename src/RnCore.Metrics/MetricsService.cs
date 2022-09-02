using RnCore.Logging;
using RnCore.Metrics.Abstractions;
using RnCore.Metrics.Builders;
using RnCore.Metrics.Models;
using RnCore.Metrics.Outputs;

namespace RnCore.Metrics;

public interface IMetricsService
{
  void Submit<TBuilder>(ICoreMetricBuilder<TBuilder> builder);
  void Submit(RnCoreMetric coreMetric);
  Task SubmitAsync(RnCoreMetric coreMetric);
  Task SubmitAsync<TBuilder>(ICoreMetricBuilder<TBuilder> builder);
}

public class MetricsService : IMetricsService
{
  private readonly ILoggerAdapter<MetricsService> _logger;
  private readonly IDateTimeAbstraction _dateTime;
  private readonly RnMetricsConfig _config;
  private readonly List<IMetricOutput> _outputs;

  public MetricsService(
    ILoggerAdapter<MetricsService> logger,
    IDateTimeAbstraction dateTime,
    IEnumerable<IMetricOutput> outputs,
    IRnMetricsConfigProvider configProvider)
  {
    _logger = logger;
    _dateTime = dateTime;
    _config = configProvider.Provide();

    if (!_config.Enabled)
    {
      _outputs = new List<IMetricOutput>();
      _logger.LogInformation("Metric service disabled (via config)");
      return;
    }

    _outputs = LoadMetricOutputs(outputs);
  }


  public void Submit<TBuilder>(ICoreMetricBuilder<TBuilder> builder)
  {
    if (!_config.Enabled)
      return;

    Submit(builder.Build());
  }

  public void Submit(RnCoreMetric coreMetric)
  {
    if (!_config.Enabled)
      return;

    SubmitAsync(coreMetric)
      .ConfigureAwait(false)
      .GetAwaiter()
      .GetResult();
  }

  public async Task SubmitAsync(RnCoreMetric coreMetric)
  {
    if (!_config.Enabled)
      return;

    var finalizedMetric = FinalizeMetric(coreMetric);
    foreach (var output in _outputs)
    {
      await output.SubmitMetric(finalizedMetric);
    }
  }

  public async Task SubmitAsync<TBuilder>(ICoreMetricBuilder<TBuilder> builder)
  {
    if (!_config.Enabled)
      return;

    await SubmitAsync(builder.Build());
  }

  private List<IMetricOutput> LoadMetricOutputs(IEnumerable<IMetricOutput> outputs)
  {
    try
    {
      var enabledOutputs = outputs.Where(x => x.Enabled).ToList();

      // No enabled outputs
      if (enabledOutputs.Count == 0)
      {
        _logger.LogWarning("No enabled outputs, disabling metric service");
        _config.Enabled = false;
        return new List<IMetricOutput>();
      }

      // We are good to go
      _logger.LogInformation("Metric service running with {count} output(s)",
        enabledOutputs.Count);

      return enabledOutputs;
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Error loading metric outputs: {message}. {stack}",
        ex.Message);

      _config.Enabled = false;
      return new List<IMetricOutput>();
    }
  }

  private RnCoreMetric FinalizeMetric(RnCoreMetric coreMetric)
  {
    var measurement = coreMetric.Measurement;

    if (_config.Overrides.ContainsKey(coreMetric.Measurement))
      measurement = _config.Overrides[coreMetric.Measurement];

    return coreMetric
      .WithDate(_dateTime.UtcNow)
      .SetTag("environment", _config.Environment)
      .SetTag("application", _config.Application)
      .UpdateMeasurement(GenerateMeasurement(measurement));
  }

  private string GenerateMeasurement(string measurement)
  {
    return _config.Template
      .Replace("{app}", _config.Application)
      .Replace("{measurement}", measurement);
  }
}
