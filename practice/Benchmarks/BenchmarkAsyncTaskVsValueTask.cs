using BenchmarkDotNet.Attributes;

namespace Practice.Benchmarks;

[MemoryDiagnoser]
public class BenchmarkAsyncTaskVsValueTask
{
    [Benchmark]
    public async Task<int> Task_CompletedSynchronously()
    {
        return await GetNumberTask();
    }

    private static Task<int> GetNumberTask()
    {
        return Task.FromResult(42); // Returns a completed task with the result 42
    }

    [Benchmark]
    public async ValueTask<int> ValueTask_CompletedSynchronously()
    {
        return await GetNumberValueTask();
    }

    private static ValueTask<int> GetNumberValueTask()
    {
        return ValueTask.FromResult(42);
    }

    [Benchmark]
    public async Task<int> Task_ActuallyAsync()
    {
        return await GetNumberTaskAsync();
    }

    private static async Task<int> GetNumberTaskAsync()
    {
        await Task.Yield();
        return 42;
    }

    [Benchmark]
    public async ValueTask<int> ValueTask_ActuallyAsync()
    {
        return await GetNumberValueTaskAsync();
    }

    private static async ValueTask<int> GetNumberValueTaskAsync()
    {
        await Task.Yield(); // Continue asynchronously
        return 42;
    }
}
