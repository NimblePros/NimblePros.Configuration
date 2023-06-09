using System.Collections.Concurrent;

using NimblePros.Configuration.Tests.Helpers;
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
    var configuration = inMemoryCollection.SetupConfiguration();

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
    var configuration = inMemoryCollection.SetupConfiguration();

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
}