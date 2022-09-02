using RnCore.Metrics.Extensions;
using RnCore.Metrics.Models;

namespace RnCore.Metrics.Builders;

public sealed class ServiceMetricBuilder : CoreMetricBuilder<ServiceMetricBuilder>
{
  private int _queryCount;
  private int _resultsCount;
  private string _serviceName = string.Empty;
  private string _methodName = string.Empty;
  private string _category = string.Empty;
  private string _subCategory = string.Empty;

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

  public ServiceMetricBuilder WithCategory(string category, string subCategory, bool skipToLower = true)
  {
    _category = skipToLower ? category : category.LowerTrim();
    _subCategory = skipToLower ? subCategory : subCategory.LowerTrim();
    return this;
  }

  public ServiceMetricBuilder WithQueryCount(int queryCount)
  {
    _queryCount = queryCount;
    return this;
  }

  public ServiceMetricBuilder IncrementQueryCount(int amount = 1)
  {
    _queryCount += amount;
    return this;
  }

  public ServiceMetricBuilder WithResultsCount(int resultsCount)
  {
    _resultsCount = resultsCount;
    return this;
  }

  public ServiceMetricBuilder IncrementResultsCount(int amount = 1)
  {
    _resultsCount += amount;
    return this;
  }

  public ServiceMetricBuilder CountResult(object? result = null)
  {
    if (result != null)
      _resultsCount += 1;

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
      .AddAction(m => { m.SetTag("service_method", _methodName, true); })
      .AddAction(m => { m.SetTag("category", _category, true); })
      .AddAction(m => { m.SetTag("sub_category", _subCategory, true); })
      .AddAction(m => { m.SetField("query_count", _queryCount); })
      .AddAction(m => { m.SetField("results_count", _resultsCount); });

    return base.Build();
  }
}

