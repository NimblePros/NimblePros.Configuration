using System.Collections.Concurrent;

using NimblePros.Configuration.Ninject;
using NimblePros.Configuration.Tests.Helpers;
using NimblePros.Configuration.Unity;

using Ninject;

namespace NimblePros.Configuration.Tests.Unity;

public class KernelExtensionsTests
{
  [Fact]
  public void AddSingletonConfig()
  {
    // Arrange
    IKernel kernel = new StandardKernel();
    var inMemoryCollection = new ConcurrentDictionary<string, string?>
    {
      ["TestKey"] = "Initial"
    };
    var configuration = inMemoryCollection.SetupConfiguration();

    // Act
    kernel.AddSingletonConfig<TestConfig>(configuration);

    var config1 = kernel.Get<TestConfig>();

    inMemoryCollection["TestKey"] = "Updated";
    configuration.Reload();

    var config2 = kernel.Get<TestConfig>();

    // Assert
    config1.Should().NotBeNull();
    config1.TestKey.Should().Be("Initial");
    config2.Should().NotBeNull();
    config2?.TestKey.Should().Be("Initial");
    config1.Should().BeSameAs(config2);
  }

  [Fact]
  public void AddScopedConfig()
  {
    // Arrange
    IKernel kernel = new StandardKernel();
    var inMemoryCollection = new ConcurrentDictionary<string, string?>
    {
      ["TestKey"] = "Initial"
    };
    var configuration = inMemoryCollection.SetupConfiguration();

    // Act
    kernel.AddTransientConfig<TestConfig>(configuration);

    var config1 = kernel.Get<TestConfig>();

    inMemoryCollection["TestKey"] = "Updated";
    configuration.Reload();

    var config2 = kernel.Get<TestConfig>();

    // Assert
    config1.Should().NotBeNull();
    config1?.TestKey.Should().Be("Initial");
    config2.Should().NotBeNull();
    config2?.TestKey.Should().Be("Updated");
    config1.Should().NotBeSameAs(config2);
  }
}