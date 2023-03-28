using Microsoft.Extensions.Configuration;

namespace NimblePros.Configuration
{
  public static class LegacyConfigurationExtensions
  {
    public static IConfigurationBuilder AddLegacyConfiguration(this IConfigurationBuilder builder)
    {
      return builder.Add(new LegacyConfigurationSource());
    }
  }
}