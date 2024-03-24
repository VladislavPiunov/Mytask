namespace Notifications.API.Model
{
    public interface INotification
    {
        string Message { get; set; }

        string DateTime { get; set; }
    }
}
