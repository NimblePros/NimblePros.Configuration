using Microsoft.Extensions.Configuration;

using NimblePros.Configuration.Legacy.Tests.Helpers;
using NimblePros.Configuration.StructureMap;

using NUnit.Framework;

using StructureMap;

namespace NimblePros.Configuration.Legacy.Tests.StructureMap
{
  public class ContainerExtensionsTests
  {
    [Test]
    public void AddSingletonConfig()
    {
      // Arrange
      var container = new Container();
      var configuration = new ConfigurationBuilder().AddLegacyConfiguration().Build();

      // Act
      container.AddSingletonConfig<TestConfig>(configuration);

      TestConfig config1;
      TestConfig config2;

      using (var nested1 = container.GetNestedContainer())
      {
        config1 = nested1.GetInstance<TestConfig>();
      }

      using (var nested2 = container.GetNestedContainer())
      {
        config2 = nested2.GetInstance<TestConfig>();
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
      var container = new Container();
      var configuration = new ConfigurationBuilder().AddLegacyConfiguration().Build();

      // Act
      container.AddScopedConfig<TestConfig>(configuration);

      TestConfig config1;
      TestConfig config2;

      using (var nested1 = container.GetNestedContainer())
      {
        config1 = nested1.GetInstance<TestConfig>();
      }

      using (var nested2 = container.GetNestedContainer())
      {
        config2 = nested2.GetInstance<TestConfig>();
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