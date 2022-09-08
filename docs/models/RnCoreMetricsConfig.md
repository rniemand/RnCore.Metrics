# RnCoreMetricsConfig

Global configuration used with **RnCore.Metrics**.

## JSON Configuration

If you are using the default implimentation of the `IRnCoreMetricsConfigProvider`

```json
{
  "Rn.Metrics": {
    "application": "MyApplication",
    "enabled": true,
    "enableConsoleOutput": true,
    "overrides": { "metric1": "renamed_metric1" },
    "environment": "development",
    "template": "{app}/{measurement}"
  }
}
```

## Configuration Properties

| Name | Type | Required | Default | Details |
| --- | --- | --- | --- | :--- |
| application | string | Required | `string.Empty` | The value to use when setting the `application` metric tag. |
| enabled | bool | Optional | `false` | Enables the metrics service. |
| enableConsoleOutput | bool | Optional | `false` | Enables the [ConsoleMetricOutput](./outputs/ConsoleMetricOutput.md) when set to `true` |
| environment | string | Optional | `production` | Value to use when setting the `environment` metric tag. |
| overrides | object | Optional | `{}` | Dictionary containing specific metric value overrides |
| template | string | Optional | `{app}/{measurement}` | Template to use when generating the final metric `Measurement` value. |

### Measurement Templating

The following values are available for use with the `template` value:

- `{app}` - will be replaced with the application name
- `{measurement}` - will be replaced with the original measurement
