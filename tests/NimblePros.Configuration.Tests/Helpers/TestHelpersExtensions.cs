using System.Collections.Concurrent;

using Microsoft.Extensions.Configuration;

namespace NimblePros.Configuration.Tests.Helpers;

public static class TestHelpersExtensions
{
  public static IConfigurationRoot SetupConfiguration(this ConcurrentDictionary<string, string?> inMemoryCollection)
  {
    var configurationBuilder = new ConfigurationBuilder();
    var testConfigurationSource = new TestConfigurationSource(inMemoryCollection);
    configurationBuilder.Add(testConfigurationSource);
    return configurationBuilder.Build();
  }
}