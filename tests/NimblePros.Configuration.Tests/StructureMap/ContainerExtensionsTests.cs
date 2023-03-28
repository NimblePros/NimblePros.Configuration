using System.Collections.Concurrent;

using NimblePros.Configuration.StructureMap;
using NimblePros.Configuration.Tests.Helpers;
using StructureMap;

namespace NimblePros.Configuration.Tests.StructureMap;

public class ContainerExtensionsTests
{
  [Fact]
  public void AddSingletonConfig()
  {
    // Arrange
    var container = new Container();
    var inMemoryCollection = new ConcurrentDictionary<string, string?>
    {
      ["TestKey"] = "Initial"
    };
    var configuration = inMemoryCollection.SetupConfiguration();

    // Act
    container.AddSingletonConfig<TestConfig>(configuration);

    TestConfig? config1;
    TestConfig? config2;

    using (var nested1 = container.GetNestedContainer())
    {
      config1 = nested1.GetInstance<TestConfig>();
    }

    inMemoryCollection["TestKey"] = "Updated";
    configuration.Reload();

    using (var nested2 = container.GetNestedContainer())
    {
      config2 = nested2.GetInstance<TestConfig>();
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
    var container = new Container();
    var inMemoryCollection = new ConcurrentDictionary<string, string?>
    {
      ["TestKey"] = "Initial"
    };
    var configuration = inMemoryCollection.SetupConfiguration();

    // Act
    container.AddScopedConfig<TestConfig>(configuration);

    TestConfig? config1;
    TestConfig? config2;

    using (var nested1 = container.GetNestedContainer())
    {
      config1 = nested1.GetInstance<TestConfig>();
    }

    inMemoryCollection["TestKey"] = "Updated";
    configuration.Reload();

    using (var nested2 = container.GetNestedContainer())
    {
      config2 = nested2.GetInstance<TestConfig>();
    }

    // Assert
    Assert.NotNull(config1);
    Assert.Equal("Initial", config1?.TestKey);
    Assert.NotNull(config2);
    Assert.Equal("Updated", config2?.TestKey);
    Assert.NotSame(config1, config2);
  }
}