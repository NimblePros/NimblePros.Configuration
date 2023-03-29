# NimblePros.Configuration

NimblePros.Configuration is a .NET library that provides extension methods to enable the use of the Options
configuration pattern with various .NET Inversion of Control (IoC) containers. The project is built on top of
`Microsoft.Extensions.Configuration` but does not use `Microsoft.Extensions.Options` due to the reasons explained in
this [article](https://www.dabrowski.space/posts/asp.net-options-why-you-should-not-use-it/). The library targets
.NET Standard 2.0, making it compatible with a wide range of .NET projects.

## Features

- Extension methods for easy integration with popular IoC containers
- Seamless configuration of your application settings using the Options pattern
- Avoids the issues and limitations associated with `Microsoft.Extensions.Options`
- Provides a bridge between `System.Configuration.ConfigurationManager` and `Microsoft.Extensions.Configuration`, making it easy to smoothly
migrate from the former to the latter

## Getting Started

### .NET Core

To use NimblePros.Configuration and the Options pattern in a .NET Core project, follow these steps:

1. Install the `NimblePros.Configuration.*` NuGet package for the IoC container of your choice. For example, to use
NimblePros.Configuration with Autofac, install the `NimblePros.Configuration.Autofac` package.

2. Define a class representing a section of your application settings, where the property names correspond to the
keys in the configuration file. For example, if your configuration file contains the following section:

```json
"mySettings": {
    "setting1": "value1",
    "setting2": "value2"
}
```

then you can define a class like this:

```csharp
public class MySettings
{
    public string Setting1 { get; set; }
    public string Setting2 { get; set; }
}
```

3. Register the configuration section with the IoC container. For example, to register the `MySettings` section with
Autofac, use the following code:

```csharp
// Autofac ContainerBuilder
var builder = new ContainerBuilder();

// Part of Microsoft.Extensions.Configuration
// Depending on your project, you may not need to create and configure a new instance of ConfigurationBuilder
var configurationBuilder = new ConfigurationBuilder();
var configuration = configurationBuilder.AddJsonFile("appsettings.json").Build();

// Register the configuration section with Autofac using one of the extension methods
// provided by NimblePros.Configuration.Autofac
containerBuilder.AddSingletonConfig<MySettings>(configuration);

var container = containerBuilder.Build();
```

The code would be very similar for a different IOC container and the extension methods provided by NimblePros.Configuration.*
all have the same name for each package:

- `AddSingletonConfig` - Registers the configuration section as a singleton
- `AddScopedConfig` - Registers the configuration section as a scoped service
- `AddTransientConfig` - Registers the configuration section as a transient service

4. Inject the configuration section into your application. For example, to inject the `MySettings` section into an ASP.NET Core
controller, use the following code:

```csharp
public class MyController : Controller
{
    private readonly MySettings _mySettings;

    public MyController(MySettings mySettings)
    {
        _mySettings = mySettings;
    }

    public IActionResult Index()
    {
        return View(_mySettings);
    }
}
```

Your IOC container will automatically resolve the `MySettings` instance and inject it into the controller.

### .NET Framework

To use NimblePros.Configuration and the Options pattern in a .NET Framework project, you would follow the above steps
with a few minor additions:

1. In addition to installing the `NimblePros.Configuration.*` package for the IoC container of your choice,
you would also need to install the `NimblePros.Configuration` which provides the extension method allowing
you to pass the configuration contained in your app.config/web.config file to the Microsoft.Extensions.Configuration
configuration builder by wrapping `System.Configuration.ConfigurationManager`.

2. Register the legacy configuration builder with the IoC container. For example, to register the legacy configuration
builder with Autofac, use the following code:

```csharp
// Autofac ContainerBuilder
var builder = new ContainerBuilder();

// Part of Microsoft.Extensions.Configuration
var configurationBuilder = new ConfigurationBuilder();
var configuration = configurationBuilder.AddLegacyConfiguration().Build();
                                                ^
                                                |
                                                +-- This is the extension method provided by NimblePros.Configuration
// Register the configuration section with Autofac using one of the extension methods
// provided by NimblePros.Configuration.Autofac
containerBuilder.AddSingletonConfig<MySettings>(configuration);

var container = containerBuilder.Build();
```

## Supported IoC Containers

- [Microsoft.Extensions.DependencyInjection](https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection) - NimblePros.Configuration.Core
- [Autofac](https://autofac.org/) - NimblePros.Configuration.Autofac
- [Castle Windsor](http://www.castleproject.org/projects/windsor/) - NimblePros.Configuration.Windsor
- [Ninject](http://www.ninject.org/) - NimblePros.Configuration.Ninject
- [StructureMap](http://structuremap.github.io/) - NimblePros.Configuration.StructureMap
- [Unity](http://unitycontainer.org/) - NimblePros.Configuration.Unity

To use NimblePros.Configuraiton with a specific IoC container, install the corresponding NuGet package, and follow
the container-specific integration steps provided in the package documentation.

## Contributing

We welcome contributions to NimblePros.Configuration. If you'd like to contribute, please submit a pull request or create an issue to discuss your ideas.

## License

NimblePros.Configuration is licensed under the MIT license. See the [LICENSE](./LICENSE.md) file for more details.
