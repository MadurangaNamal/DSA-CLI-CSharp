#nullable enable

namespace practice.practiceFiles.DesignPatterns;

public class Computer
{
    public string? CPU { get; set; }
    public string? RAM { get; set; }
    public string? Storage { get; set; }
    public string? GPU { get; set; }
    public string? Motherboard { get; set; }
    public string? PowerSupply { get; set; }
    public bool HasWiFi { get; set; }
    public bool HasBluetooth { get; set; }

    public override string? ToString()
    {
        return $"Computer Specs:\n" +
               $"CPU: {CPU}\n" +
               $"RAM: {RAM}\n" +
               $"Storage: {Storage}\n" +
               $"GPU: {GPU}\n" +
               $"Motherboard: {Motherboard}\n" +
               $"Power Supply: {PowerSupply}\n" +
               $"WiFi: {HasWiFi}\n" +
               $"Bluetooth: {HasBluetooth}";
    }
}

