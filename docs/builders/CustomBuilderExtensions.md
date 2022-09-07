# Custom Metric Builder Extensions
When creating a custom metric builder extension you can use the following constraint allow usage of your extension on all metric builders.

```cs
public static TBuilder CustomMethod<TBuilder>(this TBuilder builder, ...)
    where TBuilder : IBaseMetricBuilder<TBuilder> {}
```

Using this pattern will give you the most flexibility when it comes to working with custom metric builders.

## Sample Extensions
Below are some example extensions that may be useful in your project:

### WithInt()
Creates and populates the given `field` with the provided int value.

```cs
public static class BaseMetricBuilderExtensions
{
  public static TBuilder WithInt<TBuilder>(this TBuilder builder, string field, int value)
    where TBuilder : IBaseMetricBuilder<TBuilder>
  {
    builder.AddAction(m => { m.SetField(field, value); });
    return builder;
  }
}
```

### WithCallCount()
Builds on the `.WithInt()` extension adding a ficticious `call_count` field to the provided metric.

```cs
public static class BaseMetricBuilderExtensions
{
  public static TBuilder WithCallCount<TBuilder>(this TBuilder builder, int callCount = 1)
    where TBuilder : IBaseMetricBuilder<TBuilder> => builder.WithInt("call_count", callCount);
}
```

### WithCategory()
Creates and sets the values of the `category` and `sub_category` tags making use of the global `.WithTag()` extension method.

```cs
public static class BaseMetricBuilderExtensions
{
  public static TBuilder WithCategory<TBuilder>(this TBuilder builder, string category, string subCategory, bool skipToLower = false)
    where TBuilder : IBaseMetricBuilder<TBuilder> => builder
      .WithTag("category", category, skipToLower)
      .WithTag("sub_category", subCategory, skipToLower);
}
```
