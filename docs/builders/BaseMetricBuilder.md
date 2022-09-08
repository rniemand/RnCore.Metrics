# BaseMetricBuilder
More to come...

Uses the `IBaseMetricBuilder` interface:

```cs
public interface IBaseMetricBuilder<TBuilder>
{
  IBaseMetricBuilder<TBuilder> AddAction(Action<RnMetric> action);
  void SetSuccess(bool success);
  RnMetric Build();
}
```

## Fields

None

## Tags

- success
- has_ex
- ex_name
