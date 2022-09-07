using RnCore.Metrics.Models;

namespace RnCore.Metrics.Builders;

public interface IBaseMetricBuilder<TBuilder>
{
  IBaseMetricBuilder<TBuilder> AddAction(Action<RnMetric> action);
  void SetSuccess(bool success);
  RnMetric Build();
}
