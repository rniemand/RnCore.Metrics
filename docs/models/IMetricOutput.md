# IMetricOutput
Defines an interface to use when creating a **custom metric output**.

You can refer to the **[RnCore.Metrics.InfluxDb](https://github.com/rniemand/RnCore.Metrics/tree/master/src/RnCore.Metrics.InfluxDb)** project for an example of how to use this interface.

## Interface Overview

```cs
public interface IMetricOutput
{
  bool Enabled { get; }
  string Name { get; }

  Task SubmitMetric(RnMetric metric);
  Task SubmitMetrics(List<RnMetric> metrics);
}
```

### Submit Methods
The `SubmitMetric()` and `SubmitMetrics()` methods are called by the [MetricsService](./models/MetricsService.md) whenever there are new metrics that need to be handled by your output, ideally you should handle all given metrics in a non-blocking manner to avoid application slow down.
