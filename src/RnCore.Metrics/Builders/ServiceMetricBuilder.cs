using RnCore.Metrics.Extensions;
using RnCore.Metrics.Models;

namespace RnCore.Metrics.Builders;

public sealed class ServiceMetricBuilder : CoreMetricBuilder<ServiceMetricBuilder>
{
  private string _serviceName = string.Empty;
  private string _methodName = string.Empty;

  public ServiceMetricBuilder()
    : base("service_call")
  {
    SetSuccess(true);
  }

  public ServiceMetricBuilder(string service, string method)
    : this()
  {
    ForService(service, method);
  }

  public ServiceMetricBuilder ForService(string service, string method, bool skipToLower = true)
  {
    _serviceName = skipToLower ? service : service.LowerTrim();
    _methodName = skipToLower ? method : method.LowerTrim();
    return this;
  }

  public ServiceMetricBuilder WithException(Exception ex)
  {
    SetException(ex);
    return this;
  }
  
  public override RnCoreMetric Build()
  {
    // Append required metric tags
    AddAction(m => { m.SetTag("service_name", _serviceName, true); })
      .AddAction(m => { m.SetTag("service_method", _methodName, true); });

    return base.Build();
  }
}

