using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NimblePros.Configuration.Core
{
  public static class ServiceCollectionExtensions
  {
    static public IServiceCollection AddSingletonConfig<T>(
      this IServiceCollection serviceCollection,
      IConfiguration configuration
    )
    where T : class, new()
    {
      var config = new T();
      configuration.Bind(config);
      return serviceCollection.AddSingleton(config);
    }

    static public IServiceCollection AddScopedConfig<T>(
      this IServiceCollection serviceCollection,
      IConfiguration configuration
    )
    where T : class, new()
    {
      return serviceCollection.AddScoped(sp =>
      {
        var config = new T();
        configuration.Bind(config);
        return config;
      });
    }
  }
}