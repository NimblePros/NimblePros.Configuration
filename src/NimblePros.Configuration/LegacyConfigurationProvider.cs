
using System.Configuration;

using Microsoft.Extensions.Configuration;

namespace NimblePros.Configuration
{
  public class LegacyConfigurationProvider : ConfigurationProvider
  {
    public override void Load()
    {
      foreach (var setting in System.Configuration.ConfigurationManager.AppSettings.AllKeys)
      {
        Data[setting] = System.Configuration.ConfigurationManager.AppSettings[setting];
      }

      foreach (ConnectionStringSettings connectionString in System.Configuration.ConfigurationManager.ConnectionStrings)
      {
        Data[$"ConnectionStrings:{connectionString.Name}"] = connectionString.ConnectionString;
      }
    }
  }
}