using System.Collections.Concurrent;

using Microsoft.Extensions.Configuration;

namespace NimblePros.Configuration.Tests.Helpers;

public class TestConfigurationProvider : ConfigurationProvider
{
  private readonly ConcurrentDictionary<string, string?> _data;

  public TestConfigurationProvider(ConcurrentDictionary<string, string?> data)
  {
    _data = data;
  }

  public override void Load() => Data = new Dictionary<string, string?>(_data);
}