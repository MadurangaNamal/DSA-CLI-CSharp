using Practice.DataStructures;
using Practice.Models;

namespace Practice.Demos;

public static class GenericListUsage
{
    public static void Run()
    {
        Console.WriteLine("=== Example 1: Integer List ===");
        var integerList = new GenericList<int>();

        integerList.AddHead(10);
        integerList.AddHead(20);
        integerList.AddHead(30);
        integerList.AddHead(40);

        var enumerator = integerList.GetEnumerator();
        while (enumerator.MoveNext())
        {
            Console.WriteLine(enumerator.Current);
        }

        Console.WriteLine("\n=== Example 2: String List (using foreach) ===");
        var stringList = new GenericList<string>();

        stringList.AddHead("World");
        stringList.AddHead("Hello");
        stringList.AddHead("Welcome");

        Console.WriteLine("Strings in the list:");
        foreach (var item in stringList)
        {
            Console.WriteLine(item);
        }

        Console.WriteLine("\n=== Example 3: Custom Object List (using GetEnumerator explicitly) ===");
        var personList = new GenericList<Pupil>();

        personList.AddHead(new Pupil(1, "Alice"));
        personList.AddHead(new Pupil(2, "Bob"));
        personList.AddHead(new Pupil(3, "Charlie"));

        Console.WriteLine("People in the list:");
        var personEnumerator = personList.GetEnumerator();

        while (personEnumerator.MoveNext())
        {
            Console.WriteLine($"  {personEnumerator.Current}");
        }

        Console.WriteLine("\n=== Example 4: Using LINQ ===");
        var numberList = new GenericList<int>();
        numberList.AddHead(5);
        numberList.AddHead(3);
        numberList.AddHead(8);
        numberList.AddHead(1);

        var orderedNumbers = numberList.OrderBy(x => x).ToList();

        Console.WriteLine("Numbers sorted (LINQ uses GetEnumerator internally):");
        foreach (var num in orderedNumbers)
        {
            Console.WriteLine($"  {num}");
        }
    }
}
