using BenchmarkDotNet.Attributes;

namespace Practice.Benchmarks;

[MemoryDiagnoser]
public class ClassVsStructBenchmark
{
    [Benchmark]
    public Student CreateClass()
    {
        return new Student
        {
            StudentId = 1,
            Name = "John"
        };
    }

    [Benchmark]
    public StudentStruct CreateStruct()
    {
        return new StudentStruct
        {
            StudentId = 1,
            Name = "John"
        };
    }
}

public class Student
{
    public int StudentId { get; set; }
    public string? Name { get; set; }
}
public struct StudentStruct
{
    public int StudentId { get; set; }
    public string Name { get; set; }
}