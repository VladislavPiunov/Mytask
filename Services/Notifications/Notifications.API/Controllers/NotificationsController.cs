using Microsoft.AspNetCore.Mvc;
using Notifications.API.Model;

namespace Notifications.API.Controllers
{
    [ApiController]
    [Route("api/notifications")]
    public class NotificationsController : Controller
    {
        private readonly IRedisNotificationsRepository _repository;

        public NotificationsController(IRedisNotificationsRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<ActionResult<long>> InsertNotificationAsync(string userId, [FromBody]DefaultNotification notification)
            => Ok(await _repository.InsertNotificationAsync(userId, 0, notification));

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DefaultNotification>>> GetNotificationsAsync(string userId)
            => Ok(await _repository.GetNotificationsAsync(userId));

        [HttpPut]
        public async Task<ActionResult<bool>> UpdateNotificationAsync(string userId, long index, DefaultNotification value)
            => Ok(await _repository.UpdateNotificationAsync(userId, index, value));

        [HttpDelete]
        public async Task<ActionResult<long>> DeleteNotificationAsync(string userId, DefaultNotification value)
            => Ok(await _repository.DeleteNotificationAsync(userId, value));
    }
}
