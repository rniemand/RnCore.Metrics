using RnCore.Metrics.Builders;

namespace RnCore.Metrics.Extensions;

public static class CoreMetricBuilderExtensions
{
  public static IMetricTimingToken WithTiming<TBuilder>(this TBuilder builder, string? field)
    where TBuilder : IBaseMetricBuilder<TBuilder>
  {
    if (string.IsNullOrWhiteSpace(field))
      field = "value";

    return new MetricTimingToken<TBuilder>(builder, field);
  }

  public static IMetricTimingToken WithTiming<TBuilder>(this TBuilder builder)
    where TBuilder : IBaseMetricBuilder<TBuilder> => builder.WithTiming("value");

  public static TBuilder WitInt<TBuilder>(this TBuilder builder, string field, int value)
    where TBuilder : IBaseMetricBuilder<TBuilder>
  {
    builder.AddAction(m => { m.SetField(field, value); });
    return builder;
  }

  public static TBuilder WithCallCount<TBuilder>(this TBuilder builder, int callCount = 1)
    where TBuilder : IBaseMetricBuilder<TBuilder> => builder.WitInt("call_count", callCount);

  public static TBuilder WithResultsCount<TBuilder>(this TBuilder builder, int resultsCount = 1)
    where TBuilder : IBaseMetricBuilder<TBuilder> => builder.WitInt("results_count", resultsCount);

  public static TBuilder WithCategory<TBuilder>(this TBuilder builder, string category, string subCategory, bool skipToLower = false)
    where TBuilder : IBaseMetricBuilder<TBuilder> => builder
      .WithTag("category", category, skipToLower)
      .WithTag("sub_category", subCategory, skipToLower);

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
