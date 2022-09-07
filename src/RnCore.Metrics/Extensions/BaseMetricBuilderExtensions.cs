using RnCore.Metrics.Builders;

namespace RnCore.Metrics.Extensions;

public static class BaseMetricBuilderExtensions
{
  public static IMetricTimingToken WithTiming<TBuilder>(this TBuilder builder, string? field = null)
    where TBuilder : IBaseMetricBuilder<TBuilder>
  {
    if (string.IsNullOrWhiteSpace(field))
      field = "value";

    return new MetricTimingToken<TBuilder>(builder, field);
  }
  
  public static TBuilder WithTag<TBuilder>(this TBuilder builder, string tag, string value, bool skipToLower = false)
    where TBuilder : IBaseMetricBuilder<TBuilder>
  {
    builder.AddAction(m => { m.SetTag(tag, value, skipToLower); });
    return builder;
  }

  public static TBuilder WithSuccess<TBuilder>(this TBuilder builder, bool success)
    where TBuilder : IBaseMetricBuilder<TBuilder>
  {
    builder.SetSuccess(success);
    return builder;
  }
}
