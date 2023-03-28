using Castle.MicroKernel.Lifestyle;
using Castle.Windsor;

using Microsoft.Extensions.Configuration;

using NimblePros.Configuration.Legacy.Tests.Helpers;
using NimblePros.Configuration.Windsor;

using NUnit.Framework;

namespace NimblePros.Configuration.Legacy.Tests.Windsor
{
  public class WindsorContainerExtensionsTests
  {
    [Test]
    public void AddSingletonConfig()
    {
      // Arrange
      var container = new WindsorContainer();
      var configuration = new ConfigurationBuilder().AddLegacyConfiguration().Build();

      // Act
      container.AddSingletonConfig<TestConfig>(configuration);

      TestConfig config1;
      TestConfig config2;

      using (container.BeginScope())
      {
        config1 = container.Resolve<TestConfig>();
      }

      using (container.BeginScope())
      {
        config2 = container.Resolve<TestConfig>();
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
      var container = new WindsorContainer();
      var configuration = new ConfigurationBuilder().AddLegacyConfiguration().Build();

      // Act
      container.AddScopedConfig<TestConfig>(configuration);

      TestConfig config1;
      TestConfig config2;

      using (container.BeginScope())
      {
        config1 = container.Resolve<TestConfig>();
      }

      using (container.BeginScope())
      {
        config2 = container.Resolve<TestConfig>();
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