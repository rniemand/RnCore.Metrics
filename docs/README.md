# RnCore.Metrics

Simple metrics solution for my applications.

## Basic Usage

Register via `.AddRnCoreMetrics()` against your `IServiceCollection`:

```cs
ServiceProvider serviceProvider = new ServiceCollection()
  .AddRnCoreMetrics()
  // ...
  .BuildServiceProvider();
```

Inject an instance of `IMetricsService` where you need it:

```cs
class MyService {
  private readonly IMetricsService _metrics;

  public MyService(IMetricsService metricsService) {
    _metrics = metricsService;
  }
}
```

Create and use a metric when you need to:

```cs
public void FooFunction() {
  var builder = new ServiceMetricBuilder(nameof(MyService), nameof(FooFunction))
    .WithCategory("category", "subCategory", skipToLower: true)
    .WithSuccess(true); // Default "success" to TRUE

  using (builder.WithTiming()) // Field: value
    await Task.Delay(125);

  try {
    using (builder.WithTiming("custom")) { // Field: custom
      var dbResults = await _repo.GetUsers();
      builder.WithResultsCount(dbResults.Count);
    }
  }
  catch(Exception ex) {
    // Logging etc.
    builder.WithException(ex); // Sets "success" to FLASE
  }
  finally {
    // Submit the generated metric
    await metricsService.SubmitAsync(builder);
  }
}
```
