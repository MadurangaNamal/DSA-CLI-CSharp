using BenchmarkDotNet.Attributes;

namespace Practice.Benchmarks.Tasks;

[MemoryDiagnoser]
public class CacheBenchmarkSample
{
    private CacheExample? _cacheExample;

    [GlobalSetup]
    public void setup()
    {
        _cacheExample = new CacheExample(TimeSpan.FromSeconds(1));
    }

    [BenchmarkCategory("Task sample"), Benchmark(Baseline = true)]
    public async Task<List<PersonRecord>> GetPeopleTaskExample()
    {
        return await _cacheExample!.GetPeopleTask();
    }

    [BenchmarkCategory("ValueTask sample"), Benchmark]
    public async ValueTask<List<PersonRecord>> GetPeopleValueTaskExample()
    {
        return await _cacheExample!.GetPeopleValueTask();
    }
}


