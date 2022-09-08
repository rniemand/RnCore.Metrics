using RnCore.Metrics.Models;

namespace RnCore.Metrics.Outputs;

public interface IMetricOutput
{
  bool Enabled { get; }
  string Name { get; }

  Task SubmitMetric(RnMetric metric);
  Task SubmitMetrics(List<RnMetric> metrics);
}
