namespace Practice.DelegatesAndEvents;

/// <summary>
/// A sensor class that raises events when temperature changes significantly.
/// Demonstrates the standard .NET event pattern.
/// </summary>
public class TemperatureSensor
{
    private double _currentTemperature;
    private const double ThresholdChange = 5.0;

    public event EventHandler<TemperatureChangedEventArgs>? OnTemperatureChanged;

    public double CurrentTemperature
    {
        get => _currentTemperature;
        set
        {
            if (Math.Abs(value - _currentTemperature) >= ThresholdChange)
            {
                var oldTemp = _currentTemperature;
                _currentTemperature = value;

                var args = new TemperatureChangedEventArgs
                {
                    OldTemperature = oldTemp,
                    NewTemperature = value,
                    ChangedAt = DateTime.Now
                };

                OnTemperatureChanged?.Invoke(this, args);
            }
            else
            {
                _currentTemperature = value;
            }
        }
    }

    public TemperatureSensor(double initialTemperature)
    {
        _currentTemperature = initialTemperature;
    }
}
