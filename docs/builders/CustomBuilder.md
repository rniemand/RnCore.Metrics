# Custom Metric Builders
Creating a custom metric builder for your project is as simple as following the below steps.

<!-- tabs:start -->
#### **Step: 1**
Create your metric builder class and be sure to impliment `BaseMetricBuilder<TClass>`.

```cs
public sealed class CustomMetricBuilder : BaseMetricBuilder<CustomMetricBuilder>
{
  public CustomMetricBuilder()
    : base("custom_metric")
  {
    SetSuccess(true);
  }
}
```

In this example I am calling the `.SetSuccess()` base method to ensure that the value of `success` is always **true** for my builder.

#### **Step: 2**
I highly suggest that you create a `WithException(Exception ex)` method that calls the base `SetException(ex)` method to make capturing exception information easy for the caller.

```cs
public CustomMetricBuilder WithException(Exception ex)
{
  SetException(ex);
  return this;
}
```

You can see how this is used on the [landing page](./).

#### **Step: 3**
Create and expose any builder methods that you would like:

```cs
public CustomMetricBuilder WithCustomTag(string tagValue)
{
  AddAction(m => { m.SetTag("my_tag", tagValue, skipToLower: true); });
  return this;
}

public CustomMetricBuilder WithCustomField(int value)
{
  AddAction(m => { m.SetField("my_field", tagValue, skipToLower: true); });
  return this;
}
```

#### **Step: 4**
Optionally add a `Build()` method override to handle any custom metric logic:

```cs
public override RnMetric Build()
{
  // presuming that "_myCounter" is tracked internally
  AddAction(m => { m.SetField("my_counter", _myCounter); });
  return base.Build();
}
```

#### **Full Example**
This is a watered down example of a custom metric builder:

```cs
public sealed class CustomMetricBuilder : BaseMetricBuilder<CustomMetricBuilder>
{
  public CustomMetricBuilder()
    : base("custom_metric")
  {
    SetSuccess(true);
  }

  public CustomMetricBuilder WithException(Exception ex)
  {
    SetException(ex);
    return this;
  }

  public CustomMetricBuilder WithCustomTag(string tagValue)
  {
    AddAction(m => { m.SetTag("my_tag", tagValue, skipToLower: true); });
    return this;
  }

  public CustomMetricBuilder WithCustomField(int value)
  {
    AddAction(m => { m.SetField("my_field", value); });
    return this;
  }
}
```
Assuming that the builder is used like so:

```cs
var builder = new CustomMetricBuilder()
  .WithCustomTag("Hello")
  .WithCustomField(10);

using (builder.WithTiming())
  await Task.Delay(125);

using (builder.WithTiming("timing2"))
  await Task.Delay(125);

await _metricsService.SubmitAsync(builder);
```

 When `.Build()` is called the following values will be exposed on the returned [RnMetric](./models/RnMetric.md) object:

 - **Measurement**: `custom_metric`
 - **UtcTimestamp**: `DateTime.Now` (at builder create time)
 - **Timestamp**: `DateTime.UtcNow` (at builder create time)
 - **Tags**
   - success: `true`
   - has_ex: `false`
   - ex_name: `string.Empty`
   - my_tag: `Hello`
 - **Fields**
   - my_field: `10` (int)
   - value: `~125` (long)
   - timing2: `~125` (long)

#### **Notes**
Be sure to refer to the [BaseMetricBuilder](./builders/BaseMetricBuilder.md) documentation for a list of helper methods.

<!-- tabs:end -->
