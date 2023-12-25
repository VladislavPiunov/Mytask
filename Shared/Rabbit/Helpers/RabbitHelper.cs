using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Rabbit.Helpers
{
    public static class RabbitHelper
    {
        public static IServiceCollection AddRabbitBase(this IServiceCollection service, ConfigurationManager config)
        {
            service.Configure<RabbitMqConfiguration>(config.GetSection("RabbitMq"));

            service.AddSingleton<RabbitConnectionHelper>();

            return service;
        }
    }
}
