using Notifications.API.Model;
using StackExchange.Redis;
using System.Text.Json;

namespace Notifications.API.Repositories
{
    public class RedisNotificationsRepository : IRedisNotificationsRepository
    {
        private readonly ILogger<RedisNotificationsRepository> _logger;
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _database;

        public RedisNotificationsRepository(ILoggerFactory loggerFactory, ConnectionMultiplexer redis) 
        {
            _logger = loggerFactory.CreateLogger<RedisNotificationsRepository>();
            _redis = redis;
            _database = _redis.GetDatabase();
        }

        public async Task<long> InsertNotificationAsync(string userId, long index, DefaultNotification item)
            => await _database.ListLeftPushAsync(userId, JsonSerializer.Serialize(item));

        public async Task<IEnumerable<DefaultNotification>> GetNotificationsAsync(string userId)
        {
            var count = await _database.ListLengthAsync(userId);

            if (count == 0)
                return new List<DefaultNotification>();

            return GetListByKey<DefaultNotification>(userId, count);
        }

        private IEnumerable<T> GetListByKey<T> (string key, long count)
        {
            for (var i = 0; i < count; i++)
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var item = _database.ListGetByIndex(key, i);

                yield return JsonSerializer.Deserialize<T>(item.ToString(), options);
            }
        }

        public async Task<bool> UpdateNotificationAsync(string userId, long index, DefaultNotification value)
        { 
            await _database.ListSetByIndexAsync(userId, index, JsonSerializer.Serialize(value)); 
            return true;
        }


        public async Task<long> DeleteNotificationAsync(string userId, DefaultNotification value)
            => await _database.ListRemoveAsync(userId, JsonSerializer.Serialize(value), 0);
    }
}
