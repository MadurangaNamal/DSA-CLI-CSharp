namespace Practice.DelegatesAndEvents;

/// <summary>
/// Unsubscribing and avoiding memory leaks
/// </summary>
public class NotificationManager
{
    public event Action<string>? OnNotification;

    public void SendNotification(string message)
    {
        OnNotification?.Invoke(message);
    }

    public void ClearAllSubscribers()
    {
        OnNotification = null;
    }
}
