using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using NimblePros.Configuration.Core;
using NimblePros.Configuration.Legacy.Tests.Helpers;

using NUnit.Framework;

namespace NimblePros.Configuration.Legacy.Tests
{
  public class ServiceCollectionExtensionsTests
  {
    [Test]
    public void AddSingletonConfig()
    {
      // Arrange
      var serviceCollection = new ServiceCollection();
      var configuration = new ConfigurationBuilder().AddLegacyConfiguration().Build();

      // Act
      serviceCollection.AddSingletonConfig<TestConfig>(configuration);
      var serviceProvider = serviceCollection.BuildServiceProvider();

      TestConfig config1;
      TestConfig config2;

      using (var scope1 = serviceProvider.CreateScope())
      {
        config1 = scope1.ServiceProvider.GetService<TestConfig>();
      }

      using (var scope2 = serviceProvider.CreateScope())
      {
        config2 = scope2.ServiceProvider.GetService<TestConfig>();
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
      var serviceCollection = new ServiceCollection();
      var configuration = new ConfigurationBuilder().AddLegacyConfiguration().Build();

      // Act
      serviceCollection.AddScopedConfig<TestConfig>(configuration);
      var serviceProvider = serviceCollection.BuildServiceProvider();

      TestConfig config1;
      TestConfig config2;

      using (var scope1 = serviceProvider.CreateScope())
      {
        config1 = scope1.ServiceProvider.GetService<TestConfig>();
      }

      using (var scope2 = serviceProvider.CreateScope())
      {
        config2 = scope2.ServiceProvider.GetService<TestConfig>();
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