using System.Collections.Concurrent;

using Microsoft.Extensions.Configuration;

using NimblePros.Configuration.Legacy.Tests.Helpers;
using NimblePros.Configuration.Unity;

using NUnit.Framework;

using Unity;

namespace NimblePros.Configuration.Legacy.Tests.Unity
{
  public class UnityContainerExtensionsTests
  {
    [Test]
    public void AddSingletonConfig()
    {
      // Arrange
      IUnityContainer container = new UnityContainer();
      var configuration = new ConfigurationBuilder().AddLegacyConfiguration().Build();

      // Act
      container.AddSingletonConfig<TestConfig>(configuration);

      TestConfig config1;
      TestConfig config2;

      using (var scope1 = container.CreateChildContainer())
      {
        config1 = scope1.Resolve<TestConfig>();
      }

      using (var scope2 = container.CreateChildContainer())
      {
        config2 = scope2.Resolve<TestConfig>();
      }

      // Assert
      Assert.NotNull(config1);
      Assert.AreEqual("Initial", config1?.TestKey);
      Assert.NotNull(config2);
      Assert.AreEqual("Initial", config2?.TestKey);
      Assert.AreSame(config1, config2);
    }

    [Test]
    public void AddScopedConfig()
    {
      // Arrange
      IUnityContainer container = new UnityContainer();
      var configuration = new ConfigurationBuilder().AddLegacyConfiguration().Build();

      // Act
      container.AddScopedConfig<TestConfig>(configuration);

      TestConfig config1;
      TestConfig config2;

      using (var scope1 = container.CreateChildContainer())
      {
        config1 = scope1.Resolve<TestConfig>();
      }

      using (var scope2 = container.CreateChildContainer())
      {
        config2 = scope2.Resolve<TestConfig>();
      }

      // Assert
      Assert.NotNull(config1);
      Assert.AreEqual("Initial", config1?.TestKey);
      Assert.NotNull(config2);
      Assert.AreEqual("Initial", config2?.TestKey);
      Assert.AreNotSame(config1, config2);
    }
  }
}