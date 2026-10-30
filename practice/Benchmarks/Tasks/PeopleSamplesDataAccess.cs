namespace Practice.Benchmarks.Tasks;

public class PeopleSamplesDataAccess
{
    public static async Task<List<PersonRecord>> GetPeopleAsync()
    {
        await Task.Yield(); // To simulate asynchronous operation
        return new List<PersonRecord>
        {
            new PersonRecord("Alice", 30),
            new PersonRecord("Bob", 25),
            new PersonRecord("Charlie", 35)
        };
    }
}
