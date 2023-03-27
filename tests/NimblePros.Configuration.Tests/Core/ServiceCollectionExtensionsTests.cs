using System.Collections.Concurrent;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using NimblePros.Configuration.Core;

namespace NimblePros.Configuration.Tests;

public class ServiceCollectionExtensionsTests
{
  [Fact]
  public void AddSingletonConfig()
  {
    // Arrange
    var serviceCollection = new ServiceCollection();
    var inMemoryCollection = new ConcurrentDictionary<string, string?>
    {
      ["TestKey"] = "Initial"
    };
    var configuration = SetupConfiguration(inMemoryCollection);

    // Act
    serviceCollection.AddSingletonConfig<TestConfig>(configuration);
    var serviceProvider = serviceCollection.BuildServiceProvider();

    TestConfig? config1;
    TestConfig? config2;

    using (var scope1 = serviceProvider.CreateScope())
    {
      config1 = scope1.ServiceProvider.GetService<TestConfig>();
    }

    inMemoryCollection["TestKey"] = "Updated";
    configuration.Reload();

    using (var scope2 = serviceProvider.CreateScope())
    {
      config2 = scope2.ServiceProvider.GetService<TestConfig>();
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
    var serviceCollection = new ServiceCollection();
    var inMemoryCollection = new ConcurrentDictionary<string, string?>
    {
      ["TestKey"] = "Initial"
    };
    var configuration = SetupConfiguration(inMemoryCollection);

    // Act
    serviceCollection.AddScopedConfig<TestConfig>(configuration);
    var serviceProvider = serviceCollection.BuildServiceProvider();

    TestConfig? config1;
    TestConfig? config2;

    using (var scope1 = serviceProvider.CreateScope())
    {
      config1 = scope1.ServiceProvider.GetService<TestConfig>();
    }

    inMemoryCollection["TestKey"] = "Updated";
    configuration.Reload();

    using (var scope2 = serviceProvider.CreateScope())
    {
      config2 = scope2.ServiceProvider.GetService<TestConfig>();
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