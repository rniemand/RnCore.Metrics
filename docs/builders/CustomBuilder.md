# Creating a custom Metric Builder

Creating a custom metric builder for your project is as simple as following the below steps.

<!-- tabs:start -->

#### **Step: 1**

Create your metric builder class and be sure to impliment `CoreMetricBuilder<TClass>`.

```cs
public sealed class CustomMetricBuilder : CoreMetricBuilder<CustomMetricBuilder>
{
  public CustomMetricBuilder()
    : base("custom_builder")
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
  AddAction(m => { m.SetField("my_field", value); });
  return this;
}

public CustomMetricBuilder IncrementCounter(int amount = 1)
{
  _myCounter += amount;
  return this;
}
```

#### **Step: 4**

Finally add a `Build()` method to handle any custom logic that you may need to handle:

```cs
public override RnCoreMetric Build()
{
  AddAction(m => { m.SetField("my_counter", _myCounter); });
  return base.Build();
}
```

<!-- tabs:end -->

> [!TIP]
> Be sure to check out the [CoreMetricBuilderExtensions](./builders/CoreMetricBuilderExtensions.md) class as it provides a lot of useful default builder methods that can be applied to your metric builder.
