using RnCore.Metrics.Models;

namespace RnCore.Metrics.Builders;

public abstract class BaseMetricBuilder<TBuilder> : IBaseMetricBuilder<TBuilder>
{
  private readonly List<Action<RnMetric>> _actions = new();
  private readonly string _measurement;

  private bool _success;
  private bool _hasException;
  private string _exName = string.Empty;

  protected BaseMetricBuilder(string measurement)
  {
    _measurement = measurement;
  }

  public IBaseMetricBuilder<TBuilder> AddAction(Action<RnMetric> action)
  {
    _actions.Add(action);
    return this;
  }

  public void SetSuccess(bool success)
  {
    _success = success;
  }

  protected void SetException(Exception ex) =>
    SetException(ex.GetType().Name);

  protected void SetException(string exceptionName)
  {
    SetSuccess(false);
    _hasException = true;
    _exName = exceptionName;
  }

  public virtual RnMetric Build()
  {
    // Ensure that core fields and tags exist
    AddAction(m => { m.SetTag("success", _success); })
      .AddAction(m => { m.SetTag("has_ex", _hasException); })
      .AddAction(m => { m.SetTag("ex_name", _exName, true); });

    // Compile and build the metric
    var metric = new RnMetric(_measurement);
    _actions.ForEach(a => a(metric));
    return metric;
  }
}
