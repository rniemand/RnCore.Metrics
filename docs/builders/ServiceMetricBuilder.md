# ServiceMetricBuilder

More to come.

Inherits from [BaseMetricBuilder](./builders/BaseMetricBuilder.md)

## Measurement

Defaults to: `service_call`

## Tags

- service_name
- service_method

## Fields

Nothing.

## Methods

- `.ForService(string service, string method, bool skipToLower = true)`
- `.WithCategory(string category, string subCategory, bool skipToLower = true)`
- `.WithQueryCount(int queryCount)`
- `.IncrementQueryCount(int amount = 1)`
- `.WithResultsCount(int resultsCount)`
- `.IncrementResultsCount(int amount = 1)`
- `.CountResult(object? result = null)`
- `.WithException(Exception ex)`
