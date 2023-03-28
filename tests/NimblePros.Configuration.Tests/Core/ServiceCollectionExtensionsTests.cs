using System.Collections.Concurrent;

using Microsoft.Extensions.DependencyInjection;

using NimblePros.Configuration.Core;
using NimblePros.Configuration.Tests.Helpers;

namespace NimblePros.Configuration.Tests.Core;

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
    var configuration = inMemoryCollection.SetupConfiguration();

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
    var configuration = inMemoryCollection.SetupConfiguration();

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
}