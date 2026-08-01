namespace Practice.DelegatesAndEvents;

/// <summary>
/// Represents a button control that can raise a click event.
/// </summary>
public class ButtonControl
{
    public delegate void ButtonClickedEventHandler(string buttonName, DateTime clickTime);

    public event ButtonClickedEventHandler? OnButtonClicked;

    public string Name { get; set; }

    public ButtonControl(string name)
    {
        Name = name;
    }

    // Simulate a button click
    public void Click()
    {
        Console.WriteLine($"    Button {Name} clicked!");
        OnButtonClicked?.Invoke(Name, DateTime.UtcNow);
    }
}
