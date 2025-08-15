namespace practice.practiceFiles;

public static class CollectionExtensions
{
    private static readonly Random _random = new();
    public static void ShowItems<T>(this IEnumerable<T> collection)
    {
        Console.WriteLine();
        foreach (T item in collection)
        {
            Console.WriteLine(item);
        }
    }

    public static bool IsItEmpty<T>(this ICollection<T> candidateCollection)
    {
        Console.WriteLine();
        return candidateCollection == null || !candidateCollection.Any(); // Here using Any() is faster than count() > 0
    }

    public static T GetRandomElement<T>(this ICollection<T> array)
    {
        Console.WriteLine();
        return array.ElementAt(_random.Next(array.Count));
    }

    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> candidate)
    {
        Console.WriteLine();
        return candidate.OrderBy(x => _random.NextDouble());
    }
}
