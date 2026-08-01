namespace Practice.DelegatesAndEvents;

/// <summary>
///  Multiple subscribers - multiple methods can be subscribed 
/// </summary>
public class OrderProcessing
{
    public delegate void OrderStatusChangedEventHandler(string orderId, string status);
    public event OrderStatusChangedEventHandler? OnOrderStatusChanged;

    public void UpdateOrderStatus(string orderId, string newStatus)
    {
        Console.WriteLine($"Order {orderId} status changed to: {newStatus}");
        OnOrderStatusChanged?.Invoke(orderId, newStatus);
    }
}
