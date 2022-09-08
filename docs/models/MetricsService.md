# MetricsService

More to come...

```cs
public interface IMetricsService
{
  void Submit<TBuilder>(ICoreMetricBuilder<TBuilder> builder);
  void Submit(RnMetric coreMetric);
  Task SubmitAsync(RnMetric coreMetric);
  Task SubmitAsync<TBuilder>(ICoreMetricBuilder<TBuilder> builder);
}
```
