using Elk.Serilog.RabbitMq.Client.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Elk.Serilog.RabbitMq.Client.Extensions
{
    public static class ServiceCollection
    {
        public static void AddBusinessServices(this IServiceCollection services)
        {
            services.AddTransient<IService, Service.Service>();
        }
    }
}
