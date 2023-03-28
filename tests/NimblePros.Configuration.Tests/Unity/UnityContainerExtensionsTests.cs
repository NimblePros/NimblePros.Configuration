using System.Collections.Concurrent;

using Microsoft.Extensions.Configuration;

using NimblePros.Configuration.Unity;

using Unity;

namespace NimblePros.Configuration.Tests.Unity;

public class UnityContainerExtensionsTests
{
  [Fact]
  public void AddSingletonConfig()
  {
    // Arrange
    IUnityContainer container = new UnityContainer();
    var inMemoryCollection = new ConcurrentDictionary<string, string?>
    {
      ["TestKey"] = "Initial"
    };
    var configuration = SetupConfiguration(inMemoryCollection);

    // Act
    container.AddSingletonConfig<TestConfig>(configuration);

    TestConfig? config1;
    TestConfig? config2;

    using (var scope1 = container.CreateChildContainer())
    {
      config1 = scope1.Resolve<TestConfig>();
    }

    inMemoryCollection["TestKey"] = "Updated";
    configuration.Reload();

    using (var scope2 = container.CreateChildContainer())
    {
      config2 = scope2.Resolve<TestConfig>();
    }

    // Assert
    config1.Should().NotBeNull();
    config1?.TestKey.Should().Be("Initial");
    config2.Should().NotBeNull();
    config2?.TestKey.Should().Be("Initial");
    config1.Should().BeSameAs(config2);
  }

  [Fact]
  public void AddScopedConfig()
  {
    // Arrange
    IUnityContainer container = new UnityContainer();
    var inMemoryCollection = new ConcurrentDictionary<string, string?>
    {
      ["TestKey"] = "Initial"
    };
    var configuration = SetupConfiguration(inMemoryCollection);

    // Act
    container.AddScopedConfig<TestConfig>(configuration);

    TestConfig? config1;
    TestConfig? config2;

    using (var scope1 = container.CreateChildContainer())
    {
      config1 = scope1.Resolve<TestConfig>();
    }

    inMemoryCollection["TestKey"] = "Updated";
    configuration.Reload();

    using (var scope2 = container.CreateChildContainer())
    {
      config2 = scope2.Resolve<TestConfig>();
    }

    // Assert
    config1.Should().NotBeNull();
    config1?.TestKey.Should().Be("Initial");
    config2.Should().NotBeNull();
    config2?.TestKey.Should().Be("Updated");
    config1.Should().NotBeSameAs(config2);
  }

  private static IConfigurationRoot SetupConfiguration(ConcurrentDictionary<string, string?> inMemoryCollection)
  {
    var configurationBuilder = new ConfigurationBuilder();
    var testConfigurationSource = new TestConfigurationSource(inMemoryCollection);
    configurationBuilder.Add(testConfigurationSource);
    return configurationBuilder.Build();
  }
}