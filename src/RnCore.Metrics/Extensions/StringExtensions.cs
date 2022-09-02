namespace RnCore.Metrics.Extensions;

public static class StringExtensions
{
  public static string LowerTrim(this string str) =>
    string.IsNullOrWhiteSpace(str) ? string.Empty : str.ToLower().Trim();
}
