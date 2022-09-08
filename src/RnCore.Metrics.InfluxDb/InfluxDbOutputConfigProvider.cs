using Microsoft.Extensions.Configuration;

namespace RnCore.Metrics.InfluxDb;

public interface IInfluxDbOutputConfigProvider
{
  InfluxDbOutputConfig GetConfig();
}

public class InfluxDbOutputConfigProvider : IInfluxDbOutputConfigProvider
{
  private readonly InfluxDbOutputConfig _config;

  public InfluxDbOutputConfigProvider(IConfiguration configuration)
  {
    _config = BindConfiguration(configuration);
  }

  public InfluxDbOutputConfig GetConfig() => _config;

  private static InfluxDbOutputConfig BindConfiguration(IConfiguration configuration)
  {
    var config = new InfluxDbOutputConfig();

    var section = configuration.GetSection(InfluxDbOutputConfig.ConfigKey);
    if (!section.Exists())
      return config;

    section.Bind(config);
    return config;
  }
}
