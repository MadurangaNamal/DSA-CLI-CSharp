using BenchmarkDotNet.Attributes;
using System.Text;

namespace Practice.Benchmarks;

[MemoryDiagnoser]
public class StringBenchmark
{
    [Benchmark]
    public string StringConcatenation()
    {
        string result = string.Empty;

        for (int i = 0; i < 1000; i++)
        {
            result += i;
        }

        return result;
    }

    [Benchmark]
    public string StringBuilderConcatenation()
    {
        StringBuilder sb = new(); ;

        for (int i = 0; i < 1000; i++)
        {
            sb.Append(i);
        }

        return sb.ToString();
    }
}
