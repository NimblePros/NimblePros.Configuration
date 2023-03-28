using Microsoft.Extensions.Configuration;

using StructureMap;
using StructureMap.Pipeline;

namespace NimblePros.Configuration.StructureMap
{
  public static class ContainerExtensions
  {
    public static IContainer AddSingletonConfig<T>(this IContainer container, IConfiguration configuration)
        where T : class, new()
    {
      var config = new T();
      configuration.Bind(config);
      container.Configure(c => c.For<T>().Singleton().Use(config));
      return container;
    }

    public static IContainer AddScopedConfig<T>(this IContainer container, IConfiguration configuration)
        where T : class, new()
    {
      container.Configure(c => c.For<T>().ContainerScoped().Use("ContainerScopedConfiguration", () =>
      {
        var config = new T();
        configuration.Bind(config);
        return config;
      }));
      return container;
    }
  }
}