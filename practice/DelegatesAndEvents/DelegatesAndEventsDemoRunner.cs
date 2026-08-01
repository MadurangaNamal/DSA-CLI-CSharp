using static Practice.DelegatesAndEvents.CustomDelegates;

namespace Practice.DelegatesAndEvents;

public static class DelegatesAndEventsDemoRunner
{
    public static void RunDemo()
    {
        Console.WriteLine("\n=== EVENTS AND DELEGATES DEMO ===\n");

        DemoPart1_BasicDelegates();
        DemoPart2_CustomEventPattern();
        DemoPart3_StandardEventPattern();
        DemoPart4_MulticastDelegates();
        DemoPart5_Unsubscription();
    }

    private static void DemoPart1_BasicDelegates()
    {
        Console.WriteLine("--- Basic Delegates ---\n");

        MathOperation add = (a, b) => a + b;
        MathOperation multiply = (a, b) => a * b;

        Console.WriteLine($"Add(5, 3) = {add(5, 3)}");
        Console.WriteLine($"Multiply(5, 3) = {multiply(5, 3)}");

        // Multicast delegate
        // (Only the last method's return value is kept; all previous return values are ignored/discarded)
        MathOperation chain = (a, b) => a + b;
        chain += (a, b) => { Console.WriteLine($"Chained: {a} * {b} = {a * b}"); return a * b; };

        var result = chain(4, 5);
        Console.WriteLine($"Final result: {result}\n");
    }

    private static void DemoPart2_CustomEventPattern()
    {
        Console.WriteLine("--- Custom Event Pattern ---\n");

        var button = new ButtonControl("Submit");

        // Subscribing to the event using += operator
        button.OnButtonClicked += (name, clickTime) =>
        {
            Console.WriteLine($"  Subscriber 1: Button '{name}' clicked at {clickTime:HH:mm:ss.fff}");
        };

        button.OnButtonClicked += (name, clickTime) =>
        {
            Console.WriteLine($"  Subscriber 2: Processing click for '{name}'");
        };

        // Trigger the event
        button.Click();
        Console.WriteLine();
    }

    private static void DemoPart3_StandardEventPattern()
    {
        Console.WriteLine("--- Standard Event Pattern with EventArgs ---\n");

        var sensor = new TemperatureSensor(20.0);

        // Subscribe using standard EventHandler pattern
        sensor.OnTemperatureChanged += (sender, args) =>
        {
            Console.WriteLine($"  Alert: Temperature changed from {args.OldTemperature}°C to {args.NewTemperature}°C");
            Console.WriteLine($"  Changed at: {args.ChangedAt:HH:mm:ss}");
        };

        sensor.OnTemperatureChanged += (sender, args) =>
        {
            if (args.NewTemperature > 30)
                Console.WriteLine($"  WARNING: High temperature detected!");
        };

        sensor.CurrentTemperature = 18.0; // No event (change < 5°C)
        sensor.CurrentTemperature = 26.0; // Event raised (change >= 5°C)
        sensor.CurrentTemperature = 35.0; // Event raised with warning
        Console.WriteLine();
    }

    private static void DemoPart4_MulticastDelegates()
    {
        Console.WriteLine("--- Multicast Delegates (Multiple Subscribers) ---\n");

        var orders = new OrderProcessing();

        // Multiple subscribers listening to the same event
        orders.OnOrderStatusChanged += (orderId, status) =>
        {
            Console.WriteLine($"  [Email Service] Sending notification for order {orderId}");
        };

        orders.OnOrderStatusChanged += (orderId, status) =>
        {
            Console.WriteLine($"  [SMS Service] Sending SMS for order {orderId}");
        };

        orders.OnOrderStatusChanged += (orderId, status) =>
        {
            Console.WriteLine($"  [Database] Logging status change: {status}");
        };

        orders.OnOrderStatusChanged += (orderId, status) =>
        {
            Console.WriteLine($"  [Analytics] Recording event for order {orderId}");
        };

        // All subscribers are notified
        orders.UpdateOrderStatus("ORD-001", "Shipped");
        Console.WriteLine();
    }

    private static void DemoPart5_Unsubscription()
    {
        Console.WriteLine("--- Unsubscription and Cleanup ---\n");

        var notifier = new NotificationManager();

        // Create a named handler for proper unsubscription
        void MyHandler(string message) => Console.WriteLine($"  Received: {message}");

        // Subscribe
        notifier.OnNotification += MyHandler;
        notifier.OnNotification += (msg) => Console.WriteLine($"  Secondary handler: {msg}");

        Console.WriteLine("Sending notification with 2 subscribers:");
        notifier.SendNotification("Hello World");

        // Unsubscribe specific handler
        notifier.OnNotification -= MyHandler;

        Console.WriteLine("\nAfter unsubscribing first handler:");
        notifier.SendNotification("Second message");

        notifier.ClearAllSubscribers();

        Console.WriteLine("\nAfter clearing all subscribers:");

        notifier.SendNotification("Third message (no output expected)");

        Console.WriteLine("  (No handlers subscribed)\n");
    }
}
