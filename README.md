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

## Getting Started

To use NimblePros.Configuration in your project, follow these steps:

## Supported IoC Containers

NimblePros.Configuration provides extension methods for the following IoC containers:

- [Autofac](https://autofac.org/)

To use NimblePros.Configuraiton with a specific IoC container, install the corresponding NuGet package, and follow
the container-specific integration steps provided in the package documentation.

## Contributing

We welcome contributions to NimblePros.Configuration. If you'd like to contribute, please submit a pull request or create an issue to discuss your ideas.

## License

NimblePros.Configuration is licensed under the MIT license. See the [LICENSE](./LICENSE.md) file for more details.
