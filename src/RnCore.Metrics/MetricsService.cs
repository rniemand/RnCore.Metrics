using RnCore.Logging;
using RnCore.Metrics.Abstractions;
using RnCore.Metrics.Builders;
using RnCore.Metrics.Models;
using RnCore.Metrics.Outputs;

namespace RnCore.Metrics;

public interface IMetricsService
{
  void Submit<TBuilder>(IBaseMetricBuilder<TBuilder> builder);
  void Submit(RnMetric metric);
  Task SubmitAsync(RnMetric metric);
  Task SubmitAsync<TBuilder>(IBaseMetricBuilder<TBuilder> builder);
}

public class MetricsService : IMetricsService
{
  private readonly ILoggerAdapter<MetricsService> _logger;
  private readonly IDateTimeAbstraction _dateTime;
  private readonly MetricsConfig _config;
  private readonly List<IMetricOutput> _outputs;

  public MetricsService(
    ILoggerAdapter<MetricsService> logger,
    IDateTimeAbstraction dateTime,
    IEnumerable<IMetricOutput> outputs,
    IMetricsConfigProvider configProvider)
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


  // Public methods
  public void Submit<TBuilder>(IBaseMetricBuilder<TBuilder> builder)
  {
    if (!_config.Enabled)
      return;

    Submit(builder.Build());
  }

  public void Submit(RnMetric metric)
  {
    if (!_config.Enabled)
      return;

    SubmitAsync(metric)
      .ConfigureAwait(false)
      .GetAwaiter()
      .GetResult();
  }

  public async Task SubmitAsync(RnMetric metric)
  {
    if (!_config.Enabled)
      return;

    var finalizedMetric = FinalizeMetric(metric);
    foreach (var output in _outputs)
    {
      await output.SubmitMetric(finalizedMetric);
    }
  }

  public async Task SubmitAsync<TBuilder>(IBaseMetricBuilder<TBuilder> builder)
  {
    if (!_config.Enabled)
      return;

    await SubmitAsync(builder.Build());
  }


  // Internal methods
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

  private RnMetric FinalizeMetric(RnMetric metric)
  {
    var measurement = metric.Measurement;

    if (_config.Overrides.ContainsKey(metric.Measurement))
      measurement = _config.Overrides[metric.Measurement];

    return metric
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
