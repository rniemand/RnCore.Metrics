using System.Globalization;
using RnCore.Metrics.Extensions;

namespace RnCore.Metrics.Models;

public class RnCoreMetric
{
  public string Measurement { get; private set; }
  public DateTime Timestamp { get; private set; }
  public Dictionary<string, string> Tags { get; }
  public Dictionary<string, object> Fields { get; }

  public RnCoreMetric(string measurement)
  {
    Measurement = measurement;
    Timestamp = DateTime.MinValue;
    Tags = new Dictionary<string, string>();
    Fields = new Dictionary<string, object>();
  }

  public RnCoreMetric UpdateMeasurement(string measurement)
  {
    Measurement = measurement;
    return this;
  }

  public RnCoreMetric SetTag(string tag, string value, bool skipToLower = false)
  {
    Tags[tag] = skipToLower ? value : value.LowerTrim();
    return this;
  }

  public RnCoreMetric SetTag(string tag, bool value)
  {
    Tags[tag] = value ? "true" : "false";
    return this;
  }

  public RnCoreMetric SetTag(string tag, int value)
  {
    Tags[tag] = value.ToString("D");
    return this;
  }

  public RnCoreMetric SetTag(string tag, long value)
  {
    Tags[tag] = value.ToString("D");
    return this;
  }

  public RnCoreMetric SetTag(string tag, double value)
  {
    Tags[tag] = value.ToString(CultureInfo.InvariantCulture);
    return this;
  }

  public RnCoreMetric SetTag(string tag, byte value)
  {
    Tags[tag] = value.ToString(CultureInfo.InvariantCulture);
    return this;
  }

  public RnCoreMetric SetField(string field, long value)
  {
    Fields[field] = value;
    return this;
  }

  public RnCoreMetric SetField(string field, double value)
  {
    Fields[field] = value;
    return this;
  }

  public RnCoreMetric SetField(string field, float value)
  {
    Fields[field] = value;
    return this;
  }

  public RnCoreMetric SetField(string field, int value)
  {
    Fields[field] = value;
    return this;
  }

  public RnCoreMetric SetField(string field, bool value)
  {
    Fields[field] = value;
    return this;
  }

  public RnCoreMetric SetField(string field, sbyte value)
  {
    Fields[field] = value;
    return this;
  }

  public RnCoreMetric SetField(string field, byte value)
  {
    Fields[field] = value;
    return this;
  }

  public RnCoreMetric SetField(string field, short value)
  {
    Fields[field] = value;
    return this;
  }

  public RnCoreMetric SetField(string field, ushort value)
  {
    Fields[field] = value;
    return this;
  }

  public RnCoreMetric SetField(string field, uint value)
  {
    Fields[field] = value;
    return this;
  }

  public RnCoreMetric SetField(string field, ulong value)
  {
    Fields[field] = value;
    return this;
  }

  public RnCoreMetric SetField(string field, decimal value)
  {
    Fields[field] = value;
    return this;
  }

  public RnCoreMetric SetField(string field, TimeSpan value)
  {
    Fields[field] = value;
    return this;
  }

  public RnCoreMetric WithDate(DateTime utcTimestamp)
  {
    Timestamp = utcTimestamp;
    return this;
  }
}
