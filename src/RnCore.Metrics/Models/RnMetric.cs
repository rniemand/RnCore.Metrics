using System.Globalization;
using RnCore.Metrics.Extensions;

namespace RnCore.Metrics.Models;

public class RnMetric
{
  public string Measurement { get; private set; }
  public DateTime UtcTimestamp { get; private set; }
  public DateTime Timestamp { get; private set; }
  public Dictionary<string, string> Tags { get; }
  public Dictionary<string, object> Fields { get; }

  public RnMetric(string measurement)
  {
    Measurement = measurement;
    SetTimestamp(DateTime.MinValue);
    Tags = new Dictionary<string, string>();
    Fields = new Dictionary<string, object>();
  }

  public RnMetric UpdateMeasurement(string measurement)
  {
    Measurement = measurement;
    return this;
  }

  public RnMetric SetTag(string tag, string value, bool skipToLower = false)
  {
    Tags[tag] = skipToLower ? value : value.LowerTrim();
    return this;
  }

  public RnMetric SetTag(string tag, bool value)
  {
    Tags[tag] = value ? "true" : "false";
    return this;
  }

  public RnMetric SetTag(string tag, int value)
  {
    Tags[tag] = value.ToString("D");
    return this;
  }

  public RnMetric SetTag(string tag, long value)
  {
    Tags[tag] = value.ToString("D");
    return this;
  }

  public RnMetric SetTag(string tag, double value)
  {
    Tags[tag] = value.ToString(CultureInfo.InvariantCulture);
    return this;
  }

  public RnMetric SetTag(string tag, byte value)
  {
    Tags[tag] = value.ToString(CultureInfo.InvariantCulture);
    return this;
  }

  public RnMetric SetField(string field, long value)
  {
    Fields[field] = value;
    return this;
  }

  public RnMetric SetField(string field, double value)
  {
    Fields[field] = value;
    return this;
  }

  public RnMetric SetField(string field, float value)
  {
    Fields[field] = value;
    return this;
  }

  public RnMetric SetField(string field, int value)
  {
    Fields[field] = value;
    return this;
  }

  public RnMetric SetField(string field, bool value)
  {
    Fields[field] = value;
    return this;
  }

  public RnMetric SetField(string field, sbyte value)
  {
    Fields[field] = value;
    return this;
  }

  public RnMetric SetField(string field, byte value)
  {
    Fields[field] = value;
    return this;
  }

  public RnMetric SetField(string field, short value)
  {
    Fields[field] = value;
    return this;
  }

  public RnMetric SetField(string field, ushort value)
  {
    Fields[field] = value;
    return this;
  }

  public RnMetric SetField(string field, uint value)
  {
    Fields[field] = value;
    return this;
  }

  public RnMetric SetField(string field, ulong value)
  {
    Fields[field] = value;
    return this;
  }

  public RnMetric SetField(string field, decimal value)
  {
    Fields[field] = value;
    return this;
  }

  public RnMetric SetField(string field, TimeSpan value)
  {
    Fields[field] = value;
    return this;
  }

  public RnMetric WithDate(DateTime timestamp)
  {
    SetTimestamp(timestamp);
    return this;
  }

  private void SetTimestamp(DateTime timestamp)
  {
    Timestamp = timestamp.Kind == DateTimeKind.Utc ? timestamp.ToLocalTime() : timestamp;
    UtcTimestamp = Timestamp.ToUniversalTime();
  }
}
