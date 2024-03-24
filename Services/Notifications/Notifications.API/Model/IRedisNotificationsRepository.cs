namespace Notifications.API.Model
{
    public interface IRedisNotificationsRepository
    {
        Task<long> InsertNotificationAsync(string userId, long index, DefaultNotification item);
        Task<IEnumerable<DefaultNotification>> GetNotificationsAsync(string userId);
        Task<bool> UpdateNotificationAsync(string userId, long index, DefaultNotification value);
        Task<long> DeleteNotificationAsync(string userId, DefaultNotification value);
    }
}
