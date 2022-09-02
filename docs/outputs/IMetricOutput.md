# IMetricOutput

More to come...

```cs
public interface IMetricOutput
{
  bool Enabled { get; }
  string Name { get; }

  Task SubmitMetric(RnCoreMetric metric);
  Task SubmitMetrics(List<RnCoreMetric> metrics);
}
```
