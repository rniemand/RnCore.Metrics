# BaseMetricBuilderExtensions
Below is the complete list of all built-in metric builder extensions available to any custom metric builder.

## Fields
The below methods are used to manipulate `fields` on a metric.

### WithTiming()
Provides an **[MetricTimingToken](./models/MetricTimingToken.md)** instance with the provided `field` name.

- **field** - the name of the field to set (defaults to `value`)

```cs
IMetricTimingToken WithTiming<TBuilder>(this TBuilder builder, string? field = null) {}
```

### WithField()
Sets the provided `field` vale to the given int.

- **field** - target field name
- **value** - desired value

```cs
TBuilder WithField<TBuilder>(this TBuilder builder, string field, int value) {}
TBuilder WithField<TBuilder>(this TBuilder builder, string field, long value) {}
TBuilder WithField<TBuilder>(this TBuilder builder, string field, double value) {}
TBuilder WithField<TBuilder>(this TBuilder builder, string field, float value) {}
TBuilder WithField<TBuilder>(this TBuilder builder, string field, bool value) {}
TBuilder WithField<TBuilder>(this TBuilder builder, string field, byte value) {}
TBuilder WithField<TBuilder>(this TBuilder builder, string field, short value) {}
TBuilder WithField<TBuilder>(this TBuilder builder, string field, decimal value) {}
TBuilder WithField<TBuilder>(this TBuilder builder, string field, TimeSpan value) {}
```

## Tags
The below methods are used to manipulate `tags` on a metric.

### WithTag()
Sets the provided tag on the metric.

- **tag** - the name of the tag to set
- **value** - the value of the tag
- **skipToLower** - when `true` the original value is used

```cs
TBuilder WithTag<TBuilder>(this TBuilder builder, string tag, string value, bool skipToLower = false) {}
```

### WithSuccess()
Sets the `success` tag's value.

- **success** - can be `true` or `false`.

```cs
TBuilder WithSuccess<TBuilder>(this TBuilder builder, bool success) {}
```
