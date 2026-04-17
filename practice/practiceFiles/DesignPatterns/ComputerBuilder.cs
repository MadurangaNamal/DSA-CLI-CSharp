namespace practice.practiceFiles.DesignPatterns;

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
