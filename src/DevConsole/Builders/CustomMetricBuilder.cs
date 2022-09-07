using RnCore.Metrics.Builders;

namespace DevConsole.Builders;

public sealed class CustomMetricBuilder : BaseMetricBuilder<CustomMetricBuilder>
{
  public CustomMetricBuilder()
    : base("custom_metric")
  {
    SetSuccess(true);
  }
  
  public CustomMetricBuilder WithException(Exception ex)
  {
    SetException(ex);
    return this;
  }

  public CustomMetricBuilder WithCustomTag(string tagValue)
  {
    AddAction(m => { m.SetTag("my_tag", tagValue, skipToLower: true); });
    return this;
  }
}

