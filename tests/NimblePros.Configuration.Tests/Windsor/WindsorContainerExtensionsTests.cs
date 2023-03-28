using System.Collections.Concurrent;

using Castle.MicroKernel.Lifestyle;
using Castle.Windsor;

using NimblePros.Configuration.Tests.Helpers;
using NimblePros.Configuration.Windsor;

namespace NimblePros.Configuration.Tests.Windsor;

public class WindsorContainerExtensionsTests
{
  [Fact]
  public void AddSingletonConfig()
  {
    // Arrange
    var container = new WindsorContainer();
    var inMemoryCollection = new ConcurrentDictionary<string, string?>
    {
      ["TestKey"] = "Initial"
    };
    var configuration = inMemoryCollection.SetupConfiguration();

    // Act
    container.AddSingletonConfig<TestConfig>(configuration);

    TestConfig? config1;
    TestConfig? config2;

    using (container.BeginScope())
    {
      config1 = container.Resolve<TestConfig>();
    }

    inMemoryCollection["TestKey"] = "Updated";
    configuration.Reload();

    using (container.BeginScope())
    {
      config2 = container.Resolve<TestConfig>();
    }

    // Assert
    Assert.NotNull(config1);
    Assert.Equal("Initial", config1?.TestKey);
    Assert.NotNull(config2);
    Assert.Equal("Initial", config2?.TestKey);
    Assert.Same(config1, config2);
  }

  [Fact]
  public void AddScopedConfig()
  {
    // Arrange
    var container = new WindsorContainer();
    var inMemoryCollection = new ConcurrentDictionary<string, string?>
    {
      ["TestKey"] = "Initial"
    };
    var configuration = inMemoryCollection.SetupConfiguration();

    // Act
    container.AddScopedConfig<TestConfig>(configuration);

    TestConfig? config1;
    TestConfig? config2;

    using (container.BeginScope())
    {
      config1 = container.Resolve<TestConfig>();
    }

    inMemoryCollection["TestKey"] = "Updated";
    configuration.Reload();

    using (container.BeginScope())
    {
      config2 = container.Resolve<TestConfig>();
    }

    // Assert
    Assert.NotNull(config1);
    Assert.Equal("Initial", config1?.TestKey);
    Assert.NotNull(config2);
    Assert.Equal("Updated", config2?.TestKey);
    Assert.NotSame(config1, config2);
  }
}