# BaseMetricBuilder
The abstract `BaseMetricBuilder` class should be used when creating your own [Custom Metric Builders](./builders/CustomBuilder.md), this class impliments the `IBaseMetricBuilder` interface and provides some helper methods for building metrics.

## IBaseMetricBuilder
The signatre of `IBaseMetricBuilder` is shown below, view the most recent version [here](https://github.com/rniemand/RnCore.Metrics/blob/master/src/RnCore.Metrics/Builders/IBaseMetricBuilder.cs):

```cs
public interface IBaseMetricBuilder<TBuilder>
{
  IBaseMetricBuilder<TBuilder> AddAction(Action<RnMetric> action);
  void SetSuccess(bool success);
  RnMetric Build();
}
```

## BaseMetricBuilder
This is the default implimentation of `IBaseMetricBuilder` and provides some helper methods to create your own metric builder.

### AddAction()
Used to queue up metric builder actions to be preformed when `.Build()` is called.

```cs
public IBaseMetricBuilder<TBuilder> AddAction(Action<RnMetric> action) {}
```

### SetSuccess()
Sets the `success` tag on the final metric to the provided value.

```cs
public void SetSuccess(bool success) {}
```

### SetException()
Sets the `has_ex` and `ex_name` tag values on the built metric.

```cs
protected void SetException(Exception ex) {}
protected void SetException(string exceptionName) {}
```

### Build()
Metric finilization call, preforms the following actions:

- Sets values for the `success`, `has_ex` and `ex_name` tags.
- Sets the `Measurement` property.
- Runs all queued **actions** in the order recieved.
- Returns the compiled metric for use with the [MetricsService](./models/MetricsService.md).

```cs
public virtual RnMetric Build() {}
```

## Fields & Tags
Please refer to the **[Core Concepts](./concepts.md)** section for more information on `Field` and `Tags`.
