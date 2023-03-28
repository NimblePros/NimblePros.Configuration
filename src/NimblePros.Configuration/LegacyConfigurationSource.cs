using Microsoft.Extensions.Configuration;

namespace NimblePros.Configuration
{
  public class LegacyConfigurationSource : IConfigurationSource
  {
    public IConfigurationProvider Build(IConfigurationBuilder builder) => new LegacyConfigurationProvider();
  }
}