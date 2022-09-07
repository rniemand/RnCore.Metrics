# IMetricOutput

More to come...

```cs
public interface IMetricOutput
{
  bool Enabled { get; }
  string Name { get; }

  Task SubmitMetric(RnMetric metric);
  Task SubmitMetrics(List<RnMetric> metrics);
}
```
