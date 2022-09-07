# MetricTimingToken
Metric Timing Tokens impliment the `IMetricTimingToken` interface making them disposable and are intended to be wrapped around code blocks to gain insights into the performance of a specific code or logic flow.


## Default Timing Token
The default implimentation provided in `RnCore.Metrics` looks something like this:

```cs
public class MetricTimingToken<TBuilder> : IMetricTimingToken
{
  // ...
  public MetricTimingToken(IBaseMetricBuilder<TBuilder> builder, string fieldName)
  {
    _builder = builder;
    _fieldName = fieldName;
    _stopwatch = Stopwatch.StartNew();
  }

  public void Dispose()
  {
    var elapsedMs = _stopwatch.ElapsedMilliseconds;
    _builder.AddAction(m => { m.SetField(_fieldName, elapsedMs); });
    GC.SuppressFinalize(this);
  }
}
```

When disposed a field will be added to the provided metric with the value derived from `_stopwatch.ElapsedMilliseconds`.

### Sample Usage
Below is an example use-case for the timing token:

```cs
public async Task MyAwesomeMethod() {
  try {
    var builder = new CustomMetricBuilder()
      .ForMethod(nameof(MyAwesomeMethod))
      .WithCallCount(1);

    using(builder.WithTiming("important_call")) {
      await _httpClientService.DoSomething();
    }
  }
  catch(Exception ex) {
    // Logging and exception handling...
    builder.WithException(ex);
  }
  finally {
    // Ensure that we always submit the metric
    await _metricsService.SubmitAsync(builder);
  }
}
```

This will append a custom `field` to the provided metric named **important_call** with a value of the elapsed milliseconds for the call to complete.
