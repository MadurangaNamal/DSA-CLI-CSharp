namespace practice.practiceFiles;

public class StringManipulations
{
    public static void ArrayStringTransformations()
    {
        int[] ages = { 12, 45, 65, 70, 31, 55 };
        string names = " John,Loggy,Beared ";

        Console.WriteLine(string.Concat(ages));
        Console.WriteLine(string.Join(", ", ages));

        string[] namesArray = names.Trim().Split(',');

        foreach (var item in namesArray)
        {
            Console.Write($"{item} ");
        }

        Console.WriteLine();
    }

    public static void EqualityANDOrdering()
    {
        string greet1 = "hello";
        string greet2 = "Hello";

        bool equalityResult = greet1.Equals(greet2, StringComparison.InvariantCultureIgnoreCase);
        Console.WriteLine(equalityResult);

        int comparisonResult = greet1.CompareTo(greet2);
        Console.WriteLine(comparisonResult); // -1, 0, 1 (comes before, equal, comes after)

        comparisonResult = String.Compare(greet1, greet2, StringComparison.InvariantCultureIgnoreCase);
        Console.WriteLine(comparisonResult);

        greet1 = null!;

        try
        {
            greet1.CompareTo(greet2);
        }
        catch (NullReferenceException ex)
        {
            Console.WriteLine("Exception captured!");
            Console.WriteLine($"Exception Type: {ex.GetType().Name}");
            Console.WriteLine($"Message: {ex.Message}");
        }

        Console.WriteLine(String.Equals(greet1, greet2)); // Null safe static check for equality
    }
}
