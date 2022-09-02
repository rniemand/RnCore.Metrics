# MetricsService

More to come...

```cs
public interface IMetricsService
{
  void Submit<TBuilder>(ICoreMetricBuilder<TBuilder> builder);
  void Submit(RnCoreMetric coreMetric);
  Task SubmitAsync(RnCoreMetric coreMetric);
  Task SubmitAsync<TBuilder>(ICoreMetricBuilder<TBuilder> builder);
}
```
