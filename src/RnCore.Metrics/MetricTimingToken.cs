using RnCore.Metrics.Builders;
using System.Diagnostics;

namespace RnCore.Metrics;

public interface IMetricTimingToken : IDisposable { }

public class MetricTimingToken<TBuilder> : IMetricTimingToken
{
  private readonly ICoreMetricBuilder<TBuilder> _builder;
  private readonly string _fieldName;
  private readonly Stopwatch _stopwatch;

  public MetricTimingToken(ICoreMetricBuilder<TBuilder> builder, string fieldName)
  {
    _builder = builder;
    _fieldName = fieldName;
    _stopwatch = Stopwatch.StartNew();
  }

  public void Dispose()
  {
    var elapsedMs = _stopwatch.ElapsedMilliseconds;
    _builder.AddAction(m => { m.SetField(_fieldName, elapsedMs); });
    GC.SuppressFinalize(this);
  }
}
