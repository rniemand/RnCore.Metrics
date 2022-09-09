# MetricsService
The `MetricsService` is responsible for the finilization and dispatching of metrics to all registered outputs.

## IMetricsService
You can impliment the `IMetricsService` interface should you wish to create your own service.

```cs
public interface IMetricsService
{
  void Submit<TBuilder>(ICoreMetricBuilder<TBuilder> builder);
  void Submit(RnMetric coreMetric);
  Task SubmitAsync(RnMetric coreMetric);
  Task SubmitAsync<TBuilder>(ICoreMetricBuilder<TBuilder> builder);
}
```

## Metric Finalization

### Measurement Overrides
If any metric override is registered in [Configuration](./configuration.md) that matches the current measurement name before template processing is run, the override value will be used in place of the original measurement and passed onto template formatting.

### Measurement Templating
When finalising a metric it is run through a simple templating engine that replaces special placeholders with computed values.

The default metric template string is `{app}/{measurement}`, but can be overwritted via [Configuration](./configuration.md) if desired, currently the following placeholders are supported:

| Placeholder | E.G. | Notes |
| - | - | - |
| `{app}` | myapplication | Lowercased name of your application from configuration |
| `{measurement}` | my_measurement | The original measurement name from the current metric. |

So a metric with a measurement of `user_count` for application `Foobar` using the default template string (`{app}/{measurement}`) would be transformed to: `foobar/user_count`.

### Tag and Field Injection
The default implimentation of the metric service will inject the following tags onto all finalised metrics:

- `environment`: value derived from [Configuration](./configuration.md).
- `application`: value derived from [Configuration](./configuration.md).
