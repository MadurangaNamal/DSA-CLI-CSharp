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

public class ComputerBuilder
{
    private readonly Computer _computer = new();

    public ComputerBuilder SetCPU(string cpu)
    {
        _computer.CPU = cpu;
        return this;
    }

    public ComputerBuilder SetRAM(string ram)
    {
        _computer.RAM = ram;
        return this;
    }

    public ComputerBuilder SetStorage(string storage)
    {
        _computer.Storage = storage;
        return this;
    }

    public ComputerBuilder SetGPU(string gpu)
    {
        _computer.GPU = gpu;
        return this;
    }

    public ComputerBuilder SetMotherboard(string motherboard)
    {
        _computer.Motherboard = motherboard;
        return this;
    }

    public ComputerBuilder SetPowerSupply(string powerSupply)
    {
        _computer.PowerSupply = powerSupply;
        return this;
    }

    public ComputerBuilder AddWiFi()
    {
        _computer.HasWiFi = true;
        return this;
    }

    public ComputerBuilder AddBluetooth()
    {
        _computer.HasBluetooth = true;
        return this;
    }

    public Computer Build()
    {
        return _computer;
    }
}