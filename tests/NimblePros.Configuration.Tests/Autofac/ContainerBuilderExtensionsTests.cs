
using System.Collections.Concurrent;

using Autofac;

using Microsoft.Extensions.Configuration;

using NimblePros.Configuration.Autofac;

namespace NimblePros.Configuration.Tests.Autofac;

public class ContainerBuilderExtensionsTests
{
  [Fact]
  public void AddSingletonConfig()
  {
    // Arrange
    var containerBuilder = new ContainerBuilder();
    var inMemoryCollection = new ConcurrentDictionary<string, string?>
    {
      ["TestKey"] = "Initial"
    };
    var configuration = SetupConfiguration(inMemoryCollection);

    // Act
    containerBuilder.AddSingletonConfig<TestConfig>(configuration);
    var container = containerBuilder.Build();

    TestConfig? config1;
    TestConfig? config2;

    using (var scope1 = container.BeginLifetimeScope())
    {
      config1 = scope1.Resolve<TestConfig>();
    }

    inMemoryCollection["TestKey"] = "Updated";
    configuration.Reload();

    using (var scope2 = container.BeginLifetimeScope())
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
    var containerBuilder = new ContainerBuilder();
    var inMemoryCollection = new ConcurrentDictionary<string, string?>
    {
      ["TestKey"] = "Initial"
    };
    var configuration = SetupConfiguration(inMemoryCollection);

    // Act
    containerBuilder.AddScopedConfig<TestConfig>(configuration);
    var container = containerBuilder.Build();

    TestConfig? config1;
    TestConfig? config2;

    using (var scope1 = container.BeginLifetimeScope())
    {
      config1 = scope1.Resolve<TestConfig>();
    }

    inMemoryCollection["TestKey"] = "Updated";
    configuration.Reload();

    using (var scope2 = container.BeginLifetimeScope())
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