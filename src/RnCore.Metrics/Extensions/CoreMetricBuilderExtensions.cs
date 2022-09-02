using RnCore.Metrics.Builders;

namespace RnCore.Metrics.Extensions;

public static class CoreMetricBuilderExtensions
{
  public static IMetricTimingToken WithTiming<TBuilder>(this TBuilder builder, string? field)
    where TBuilder : ICoreMetricBuilder<TBuilder>
  {
    if (string.IsNullOrWhiteSpace(field))
      field = "value";

    return new MetricTimingToken<TBuilder>(builder, field);
  }

  public static IMetricTimingToken WithTiming<TBuilder>(this TBuilder builder)
    where TBuilder : ICoreMetricBuilder<TBuilder> => builder.WithTiming("value");
}
