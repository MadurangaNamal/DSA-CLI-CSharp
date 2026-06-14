using BenchmarkDotNet.Attributes;

namespace Practice.Benchmarks;

public class ListVsHashsetSearchBenchmark
{
    private readonly List<int> _numbers = Enumerable.Range(1, 1_000_000).ToList();
    private readonly HashSet<int> _set = Enumerable.Range(1, 1_000_000).ToHashSet();

    [Benchmark]
    public bool SearchList()
    {
        return _numbers.Contains(999_999);
    }

    [Benchmark]
    public bool SearchSet()
    {
        return _set.Contains(999_999);
    }
}
