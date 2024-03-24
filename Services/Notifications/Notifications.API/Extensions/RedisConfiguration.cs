using Microsoft.AspNetCore.Http.Connections;
using StackExchange.Redis;

namespace Notifications.API.Extensions
{
    public static class RedisConfiguration
    {
        public static IServiceCollection AddRedisCache(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddSingleton(sp =>
            {
                var options = configuration
                .GetSection("Redis").Get<Redis>()
                ?? throw new ArgumentNullException(nameof(Redis));

                var connectionOptions = new ConfigurationOptions
                {
                    EndPoints = { { options.Host, options.Port } },
                    User = options.User,
                    Password = options.Password,
                    Ssl = false
                };

                return ConnectionMultiplexer.Connect(connectionOptions);
            });

            return services;
        }
    }

    public class Redis
    {
        public string Host { get; set; }

        public int Port { get; set; }

        public string User { get; set; }

        public string Password { get; set; }
    }
}
