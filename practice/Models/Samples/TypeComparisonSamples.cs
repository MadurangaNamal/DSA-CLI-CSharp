namespace Practice.Models.Samples;

public class SampleClass
{
    public int a;
    public bool b;
    public string? value;
}

public struct SampleStruct
{
    public int a;
    public bool b;
    public string value;
}

public record SampleRecord(int a, bool b, string value);
