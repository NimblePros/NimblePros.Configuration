using Autofac;

using Microsoft.Extensions.Configuration;

using NimblePros.Configuration.Autofac;
using NimblePros.Configuration.Legacy.Tests.Helpers;

using NUnit.Framework;

namespace NimblePros.Configuration.Legacy.Tests.Autofac
{
  public class ContainerBuilderExtensionsTests
  {
    [Test]
    public void AddSingletonConfig()
    {
      // Arrange
      var containerBuilder = new ContainerBuilder();
      var configuration = new ConfigurationBuilder().AddLegacyConfiguration().Build();

      // Act
      containerBuilder.AddSingletonConfig<TestConfig>(configuration);
      var container = containerBuilder.Build();

      TestConfig config1;
      TestConfig config2;

      using (var scope1 = container.BeginLifetimeScope())
      {
        config1 = scope1.Resolve<TestConfig>();
      }

      using (var scope2 = container.BeginLifetimeScope())
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
      var containerBuilder = new ContainerBuilder();
      var configuration = new ConfigurationBuilder().AddLegacyConfiguration().Build();

      // Act
      containerBuilder.AddScopedConfig<TestConfig>(configuration);
      var container = containerBuilder.Build();

      TestConfig config1;
      TestConfig config2;

      using (var scope1 = container.BeginLifetimeScope())
      {
        config1 = scope1.Resolve<TestConfig>();
      }

      using (var scope2 = container.BeginLifetimeScope())
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