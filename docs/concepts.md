# Core Concepts

## RnMetric
Represents a collected metric (measurement) for your application, and is comprides of the following parts:

- **Measurement** - description of the captured metric
- **UtcTimestamp** - the value of `DateTime.UtcNow` when the metric was created
- **Timestamp** - the value of `DateTime.Now` when the metric was created
- **Tags** - dictionary of key(`string`): value(`string`) pairs
- **Fields** - dictionary of key(`string`): value(`object`) pairs

### Tags
`Tags` are used to store contextual information about the collected metric.

E.G. the `host` machine, `environment` the application is running on etc.

These values can be used to group and filter metrics at the presentation layer.

### Fields
`Fields` are a collection of measurements relating to your collected metric.

Fields can be anything from `counters` to `timings` and are generally graphed out at the presentation layer.
