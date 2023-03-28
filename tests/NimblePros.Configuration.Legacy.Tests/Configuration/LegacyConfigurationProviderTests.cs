using System.Collections.Generic;

using NUnit.Framework;

namespace NimblePros.Configuration.Legacy.Tests
{

  public class LegacyConfigurationProviderTests
  {
    private class TestableLegacyConfigurationProvider : LegacyConfigurationProvider
    {
      public IReadOnlyDictionary<string, string> ExposedData => (IReadOnlyDictionary<string, string>)Data;
    }

    [Test]
    public void Load_AppSettings_CorrectlyLoaded()
    {
      // Arrange
      var provider = new TestableLegacyConfigurationProvider();

      // Act
      provider.Load();

      // Assert
      Assert.AreEqual("SampleValue1", provider.ExposedData["SampleKey1"]);
      Assert.AreEqual("SampleValue2", provider.ExposedData["SampleKey2"]);
      // Add more assertions for other AppSettings keys
    }

    [Test]
    public void Load_ConnectionStrings_CorrectlyLoaded()
    {
      // Arrange
      var provider = new TestableLegacyConfigurationProvider();

      // Act
      provider.Load();

      // Assert
      Assert.AreEqual("SampleConnectionString1", provider.ExposedData["ConnectionStrings:SampleConnection1"]);
      Assert.AreEqual("SampleConnectionString2", provider.ExposedData["ConnectionStrings:SampleConnection2"]);
      // Add more assertions for other ConnectionStrings keys
    }
  }
}