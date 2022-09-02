using RnCore.Metrics.Models;

namespace RnCore.Metrics.Builders;

public interface ICoreMetricBuilder<TBuilder>
{
  ICoreMetricBuilder<TBuilder> AddAction(Action<RnCoreMetric> action);
  void SetSuccess(bool success);
  RnCoreMetric Build();
}
