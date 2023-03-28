using Microsoft.Extensions.Configuration;

using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace NimblePros.Configuration.Unity
{
  public static class UnityContainerExtensions
  {
    public static IUnityContainer AddSingletonConfig<T>(this IUnityContainer container, IConfiguration configuration)
        where T : class, new()
    {
      var config = new T();
      configuration.Bind(config);
      return container.RegisterInstance(config, InstanceLifetime.Singleton);
    }

    public static IUnityContainer AddScopedConfig<T>(this IUnityContainer container, IConfiguration configuration)
        where T : class, new()
    {
      return container.RegisterFactory<T>(_ =>
      {
        var config = new T();
        configuration.Bind(config);
        return config;
      }, FactoryLifetime.Scoped);
    }
  }
}