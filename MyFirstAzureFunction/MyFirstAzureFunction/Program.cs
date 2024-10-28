using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MyFirstAzureFunction;
[ExcludeFromCodeCoverage]
public static class Program
{
    public static void Main()
    {
        var host = new HostBuilder()
            .ConfigureFunctionsWorkerDefaults()
            .ConfigureServices(services =>
            {
                ConfigureServices(ref services);
            })
            .Build();
        host.Run();
    }

    static void ConfigureServices(ref IServiceCollection services)
    {
        var startUp = new Startup();
        var projectServices = startUp.GetType()
            .Assembly
            .GetTypes()
            .Where(t => t.Namespace != null && t.Namespace.StartsWith("MyFirstAzureFunction.Implementations"));
        foreach (var serviceType in projectServices)
        {
            var interfaceType = serviceType
                .GetInterfaces()
                .FirstOrDefault(t => t.Namespace != null && t.Namespace.StartsWith("MyFirstAzureFunction.Interfaces"));
            if (interfaceType == null)
            {
                continue;
            }
            services.AddSingleton(interfaceType, serviceType);
        }
    }
}