using Castle.MicroKernel.Registration;
using Castle.Windsor;

using Microsoft.Extensions.Configuration;

namespace NimblePros.Configuration.Windsor
{
  public static class WindsorContainerExtensions
  {
    public static IWindsorContainer AddSingletonConfig<T>(this IWindsorContainer container, IConfiguration configuration)
        where T : class, new()
    {
      var config = new T();
      configuration.Bind(config);
      return container.Register(Component.For<T>().Instance(config).LifestyleSingleton());
    }

    public static IWindsorContainer AddScopedConfig<T>(this IWindsorContainer container, IConfiguration configuration)
        where T : class, new()
    {
      return container.Register(Component.For<T>().UsingFactoryMethod(kernel =>
      {
        var config = new T();
        configuration.Bind(config);
        return config;
      }).LifestyleScoped());
    }
  }
}