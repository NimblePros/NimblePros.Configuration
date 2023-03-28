using System.Collections.Concurrent;

using Microsoft.Extensions.Configuration;

namespace NimblePros.Configuration.Tests.Helpers;

public class TestConfigurationSource : IConfigurationSource
{
  private readonly ConcurrentDictionary<string, string?> _data;

  public TestConfigurationSource(ConcurrentDictionary<string, string?> data)
  {
    _data = data;
  }

  public IConfigurationProvider Build(IConfigurationBuilder builder)
  {
    return new TestConfigurationProvider(_data);
  }
}