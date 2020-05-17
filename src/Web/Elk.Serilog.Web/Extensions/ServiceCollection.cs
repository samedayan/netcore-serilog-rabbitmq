using Elk.Serilog.RabbitMq.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Elk.Serilog.RabbitMq.Web.Extensions
{
    public static class ServiceCollection
    {
        public static void AddBusinessServices(this IServiceCollection services)
        {
            services.AddTransient<IService, Service.Service>();
        }
    }
}
