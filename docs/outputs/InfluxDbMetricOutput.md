# InfluxDbMetricOutput
[InfluxDB 2](https://www.influxdata.com/) compatible output for `RnCore.Metrics`, package available on [NuGet](https://www.nuget.org/packages/RnCore.Metrics.InfluxDb).

This wrapper makes use of the [InfluxDB.Client](https://www.nuget.org/packages/InfluxDB.Client) to generate and submit `PointData` entries for each given metric.

## Installation
Add `RnCore.Metrics.InfluxDb` to your project.

```powershell
Install-Package RnCore.Metrics.InfluxDb
```

Register the output against your container:

```cs
ServiceProvider serviceProvider = new ServiceCollection()
  .AddRnCoreMetrics()
  .AddInfluxDbMetricOutput()
  .BuildServiceProvider();
```

Add in required configuration:

```json
{
  "RnCore.Metrics.InfluxDb": {
    "token": "",
    "bucket": "default",
    "org": "",
    "url": "",
    "enabled": false
  }
}
```

## Custom Configuration
If so desired, you can create your own implimentation of `IInfluxDbOutputConfigProvider` to provide configuration for this output.

Be sure to disable the default provider when calling `.AddRnCoreMetricsInfluxDb()`:

```cs
ServiceProvider serviceProvider = new ServiceCollection()
  .AddRnCoreMetrics()
  .AddInfluxDbMetricOutput(useDefaultConfigProvider: false)
  .AddSingleton<IInfluxDbOutputConfigProvider, MyConfigProvider>()
  .BuildServiceProvider();
```

Just make sure to register your provider 😊.
