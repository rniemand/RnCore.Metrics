namespace RnCore.Metrics.Abstractions;

public interface IDateTimeAbstraction
{
  DateTime UtcNow { get; }
}

public class DateTimeAbstraction : IDateTimeAbstraction
{
  public DateTime UtcNow => DateTime.UtcNow;
}
