using Microsoft.Extensions.Configuration;

using Ninject;
using Ninject.Syntax;

namespace NimblePros.Configuration.Ninject
{
  public static class KernelExtensions
  {
    public static IBindingWhenInNamedWithOrOnSyntax<T> AddSingletonConfig<T>(
          this IKernel kernel,
          IConfiguration configuration
      )
      where T : class, new()
    {
      var config = new T();
      configuration.Bind(config);
      return kernel.Bind<T>().ToConstant(config);
    }

    public static IBindingNamedWithOrOnSyntax<T> AddTransientConfig<T>(
        this IKernel kernel,
        IConfiguration configuration
    )
    where T : class, new()
    {
      return kernel.Bind<T>().ToMethod(context =>
      {
        var config = new T();
        configuration.Bind(config);
        return config;
      }).InTransientScope();
    }
  }
}