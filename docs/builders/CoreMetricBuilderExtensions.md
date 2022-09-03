# CoreMetricBuilderExtensions

## Global Methods

- `.WithTiming(string? field)` - field defaults to `value`.
- `.WithCategory(string category, string subCategory, bool skipToLower = false)`
- `.WithTag(string tag, string value, bool skipToLower = false)`
- `.WithInt(string field, int value)`
- `.WithCallCount(int callCount = 1)` - sets a `call_count` field with the provided value.
- `.WithSuccess(bool success)` - sets the `success` to the provided value.
- `.WithResultsCount(int resultsCount = 1)` - sets `results_count` field with the provided value.
