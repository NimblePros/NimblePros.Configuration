using Autofac;
using Autofac.Builder;

using Microsoft.Extensions.Configuration;

namespace NimblePros.Configuration.Autofac
{
  public static class ContainerBuilderExtensions
  {
    static public IRegistrationBuilder<T, SimpleActivatorData, SingleRegistrationStyle> AddSingletonConfig<T>(
      this ContainerBuilder containerBuilder,
      IConfiguration configuration
    )
    where T : class, new()
    {
      var config = new T();
      configuration.Bind(config);
      return containerBuilder.RegisterInstance(config).As<T>().SingleInstance();
    }

    static public IRegistrationBuilder<T, SimpleActivatorData, SingleRegistrationStyle> AddScopedConfig<T>(
      this ContainerBuilder containerBuilder,
      IConfiguration configuration
    )
    where T : class, new()
    {
      return containerBuilder.Register(ctx =>
      {
        var config = new T();
        configuration.Bind(config);
        return config;
      }).As<T>().InstancePerLifetimeScope();
    }
  }
}