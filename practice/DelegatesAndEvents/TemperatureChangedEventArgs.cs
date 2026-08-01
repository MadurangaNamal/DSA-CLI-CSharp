namespace Practice.DelegatesAndEvents;

public class TemperatureChangedEventArgs : EventArgs
{
    public double OldTemperature { get; set; }
    public double NewTemperature { get; set; }
    public DateTime ChangedAt { get; set; }
}
