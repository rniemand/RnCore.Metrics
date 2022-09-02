using RnCore.Metrics.Models;
using System.Text;

namespace RnCore.Metrics.Outputs;

public class ConsoleMetricOutput : IMetricOutput
{
  public bool Enabled { get; }
  public string Name => nameof(ConsoleMetricOutput);

  public ConsoleMetricOutput()
  {
    // TODO: re-enable this
    Enabled = false;
  }

  public async Task SubmitMetric(RnCoreMetric metric) =>
  await SubmitMetrics(new List<RnCoreMetric> { metric });

  public async Task SubmitMetrics(List<RnCoreMetric> metrics)
  {
    await Task.CompletedTask;

    foreach (var metric in metrics)
    {
      Console.ForegroundColor = ConsoleColor.Green;
      Console.WriteLine(ProcessMetric(metric));
      Console.ResetColor();
    }
  }

  private static string ProcessMetric(RnCoreMetric metric)
  {
    return new StringBuilder("[")
      .Append(metric.Timestamp.ToLocalTime().ToString("s"))
      .Append("] \"")
      .Append(metric.Measurement)
      .Append('"')
      .Append(GenerateTagsString(metric))
      .Append(GenerateFieldsString(metric))
    .ToString();
  }

  private static string GenerateTagsString(RnCoreMetric metric)
  {
    if (metric.Tags.Count == 0)
      return string.Empty;

    var tags = new List<string>();
    foreach (var (key, value) in metric.Tags)
    {
      tags.Add(new StringBuilder($"{key} => ")
        .Append(string.IsNullOrWhiteSpace(value) ? "NULL" : value)
        .ToString());
    }
    return " " + string.Join(", ", tags);
  }

  private static string GenerateFieldsString(RnCoreMetric metric)
  {
    if (metric.Fields.Count == 0)
      return string.Empty;

    var tags = new List<string>();
    foreach (var (key, value) in metric.Fields)
    {
      tags.Add($"({key} => {FieldToString(value)})");
    }

    return " " + string.Join(" ", tags);
  }

  private static string FieldToString(object field)
  {
    return field switch
    {
      string strField => strField,
      long longField => longField.ToString("D"),
      int intField => intField.ToString("D"),
      float floatField => floatField.ToString("G"),
      double doubleField => doubleField.ToString("G"),
      decimal decimalField => decimalField.ToString("G"),
      byte byteField => byteField.ToString("D"),
      short shortField => shortField.ToString("G"),
      ushort ushortField => ushortField.ToString("G"),
      uint uintField => uintField.ToString("G"),
      ulong ulongField => ulongField.ToString("G"),
      bool boolField => boolField ? "true" : "false",
      sbyte sbyteField => sbyteField.ToString("D"),
      TimeSpan tsField => tsField.ToString("g"),
      _ => throw new Exception("Field type is not supported")
    };
  }
}
