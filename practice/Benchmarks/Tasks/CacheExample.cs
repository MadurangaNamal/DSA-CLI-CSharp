namespace Practice.Benchmarks.Tasks;

/// <summary>
/// Provides caching functionality sample for retrieving person records.
/// </summary>
public class CacheExample
{
    private readonly TimeSpan _cacheLifeTime;
    private List<PersonRecord>? _cachedPeople;
    private DateTime _cacheLastRefreshTime;

    public CacheExample(TimeSpan cacheLifeTime)
    {
        _cacheLifeTime = cacheLifeTime;
    }

    public Task<List<PersonRecord>> GetPeopleTask()
    {
        if (_cachedPeople != null && DateTime.UtcNow - _cacheLastRefreshTime < _cacheLifeTime)
        {
            return Task.FromResult(_cachedPeople);
        }

        return LoadPeople();
    }

    public ValueTask<List<PersonRecord>> GetPeopleValueTask()
    {
        if (_cachedPeople != null && DateTime.UtcNow - _cacheLastRefreshTime < _cacheLifeTime)
        {
            return new ValueTask<List<PersonRecord>>(_cachedPeople);
        }

        return new ValueTask<List<PersonRecord>>(LoadPeople());
    }

    private async Task<List<PersonRecord>> LoadPeople()
    {
        _cachedPeople = await PeopleSamplesDataAccess.GetPeopleAsync();
        _cacheLastRefreshTime = DateTime.UtcNow;
        return _cachedPeople;
    }
}
