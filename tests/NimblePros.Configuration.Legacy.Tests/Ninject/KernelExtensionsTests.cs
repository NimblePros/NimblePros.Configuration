using Microsoft.Extensions.Configuration;

using NimblePros.Configuration.Legacy.Tests.Helpers;
using NimblePros.Configuration.Ninject;
using NimblePros.Configuration.Unity;

using Ninject;

using NUnit.Framework;

namespace NimblePros.Configuration.Legacy.Tests.Unity
{
  public class KernelExtensionsTests
  {
    [Test]
    public void AddSingletonConfig()
    {
      // Arrange
      IKernel kernel = new StandardKernel();
      var configuration = new ConfigurationBuilder().AddLegacyConfiguration().Build();

      // Act
      kernel.AddSingletonConfig<TestConfig>(configuration);

      var config1 = kernel.Get<TestConfig>();
      var config2 = kernel.Get<TestConfig>();

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
      IKernel kernel = new StandardKernel();
      var configuration = new ConfigurationBuilder().AddLegacyConfiguration().Build();

      // Act
      kernel.AddTransientConfig<TestConfig>(configuration);

      var config1 = kernel.Get<TestConfig>();
      var config2 = kernel.Get<TestConfig>();

      // Assert
      Assert.NotNull(config1);
      Assert.AreEqual("Initial", config1?.TestKey);
      Assert.NotNull(config2);
      Assert.AreEqual("Initial", config2?.TestKey);
      Assert.AreNotSame(config1, config2);
    }
  }
}