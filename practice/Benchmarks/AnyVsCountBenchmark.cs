using BenchmarkDotNet.Attributes;

namespace Practice.Benchmarks;

[MemoryDiagnoser]
public class AnyVsCountBenchmark
{
    private readonly List<int> _numbers = Enumerable.Range(1, 1_000_000).ToList();

    [Benchmark]
    public bool UseAny()
    {
        return _numbers.Any();
    }

    [Benchmark]
    public bool UseCount()
    {
        return _numbers.Count > 0;
    }
}
