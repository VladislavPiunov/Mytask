namespace Notifications.API.Model
{
    public class DefaultNotification : INotification
    {
        public string Message { get; set; }

        public string DateTime { get; set; }
    }
}
