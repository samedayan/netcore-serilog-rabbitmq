using Elk.Serilog.RabbitMq.Client.Common.ProjectConst;
using Microsoft.Extensions.Options;

namespace Elk.Serilog.RabbitMq.Client.Service
{
    public class Service : IService
    {
        private readonly IOptions<ProjectSettings> _settings;
        public Service(IOptions<ProjectSettings> settings)
        {
            _settings = settings;
        }
    }
}
