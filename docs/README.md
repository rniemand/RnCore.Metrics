# RnCore.Metrics

Simple metrics solution for my applications.

## Basic Usage

Register via `.AddRnCoreMetrics()` against your `IServiceCollection`:

```cs
ServiceProvider serviceProvider = new ServiceCollection()
  .AddRnCoreMetrics()
  .BuildServiceProvider();
```
