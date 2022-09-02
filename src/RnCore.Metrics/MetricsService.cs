using RnCore.Logging;

namespace RnCore.Metrics;

public interface IMetricsService
{
}

public class MetricsService : IMetricsService
{
  private readonly ILoggerAdapter<MetricsService> _logger;

  public MetricsService(ILoggerAdapter<MetricsService> logger)
  {
    _logger = logger;
  }
}
