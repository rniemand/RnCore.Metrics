# Custom Metric Builder Extensions
More to come...

```cs
public static class BaseMetricBuilderExtensions
{
  public static TBuilder WithInt<TBuilder>(this TBuilder builder, string field, int value)
    where TBuilder : IBaseMetricBuilder<TBuilder>
  {
    builder.AddAction(m => { m.SetField(field, value); });
    return builder;
  }

  public static TBuilder WithCallCount<TBuilder>(this TBuilder builder, int callCount = 1)
    where TBuilder : IBaseMetricBuilder<TBuilder> => builder.WithInt("call_count", callCount);

  public static TBuilder WithResultsCount<TBuilder>(this TBuilder builder, int resultsCount = 1)
    where TBuilder : IBaseMetricBuilder<TBuilder> => builder.WithInt("results_count", resultsCount);

  public static TBuilder WithCategory<TBuilder>(this TBuilder builder, string category, string subCategory, bool skipToLower = false)
    where TBuilder : IBaseMetricBuilder<TBuilder> => builder
      .WithTag("category", category, skipToLower)
      .WithTag("sub_category", subCategory, skipToLower);
}
```
