# ConsoleMetricOutput
Simple Console based output used for debugging logged metrics.

## Installation
This output is included as part of the `RnCore.Metrics` library, and can be enabled through configuration:

```json
{
  "Rn.Metrics": {
    "enabled": true,
    "enableConsoleOutput": true
  }
}
```

Once running all generated metrics will be logged to the `System.Console` in a similar format:

```text
Metric submitted:
  (2022-09-07T19:46:36) myapplication/custom_metric
  Tags:
    - success: false
    - has_ex: true
    - ex_name: Exception
    - environment: development
    - application: myapplication
  Fields:
    - value: 138
```

> [!NOTE]
> This output is intended for development purposes only.
