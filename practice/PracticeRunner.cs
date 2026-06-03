using Practice.Algorithms;
using Practice.Collections;
using Practice.Delegates;
using Practice.Demos;
using Practice.Models;

namespace Practice;

/// <summary>
/// Interactive menu to run individual practice demos without editing Program.cs.
/// </summary>
public static class PracticeRunner
{
    public static async Task RunChoiceFromArgsAsync(string choice) =>
        await RunChoiceAsync(choice);

    public static async Task RunAsync()
    {
        while (true)
        {
            PrintMenu();
            Console.Write("\nSelect a demo (number or Q to quit): ");
            var input = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(input) ||
                input.Equals("q", StringComparison.OrdinalIgnoreCase) ||
                input.Equals("0", StringComparison.Ordinal))
            {
                Console.WriteLine("Goodbye.");
                return;
            }

            try
            {
                await RunChoiceAsync(input);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to return to the menu...");
            Console.ReadKey(intercept: true);
            Console.WriteLine();
        }
    }

    private static void PrintMenu()
    {
        Console.Clear();
        Console.WriteLine("=== C# Playground ===");
        Console.WriteLine();
        Console.WriteLine(" 1  Custom exceptions");
        Console.WriteLine(" 2  LINQ — filter employees");
        Console.WriteLine(" 3  LINQ — department statistics");
        Console.WriteLine(" 4  LINQ — SQL-style query walkthrough");
        Console.WriteLine(" 5  Algorithms — second largest number");
        Console.WriteLine(" 6  Algorithms — first non-repeating character");
        Console.WriteLine(" 7  Algorithms — merge sorted linked lists");
        Console.WriteLine(" 8  Records & DTO illustrations");
        Console.WriteLine(" 9  Pattern matching");
        Console.WriteLine("10  Design patterns — singleton");
        Console.WriteLine("11  Design patterns — builder");
        Console.WriteLine("12  Design patterns — strategy & factory");
        Console.WriteLine("13  AutoMapper");
        Console.WriteLine("14  String manipulations");
        Console.WriteLine("15  Extension methods (collections)");
        Console.WriteLine("16  Delegates");
        Console.WriteLine("17  Document processor (parallel file read)");
        Console.WriteLine("18  Class vs struct vs record");
        Console.WriteLine(" Q  Quit");
    }

    private static async Task RunChoiceAsync(string choice)
    {
        switch (choice)
        {
            case "1":
                await PracticeDemos.CustomExceptionHandling();
                break;
            case "2":
                var filtered = LinqExercises.FilterEmployees([41, 42, 43]);
                LinqExercises.PrintEmployeesList(filtered);
                break;
            case "3":
                var stats = LinqExercises.GetDepartmentStats();
                foreach (var stat in stats)
                {
                    Console.WriteLine($"{stat.Department,-15} | Avg: {stat.AvgSalary,10:C} | Count: {stat.TotalEmployees,3}");
                }
                break;
            case "4":
                LinqExercises.PrintCustomLinqQueryResults();
                break;
            case "5":
                RunSecondLargestDemo();
                break;
            case "6":
                var ch = AlgorithmExercises.GetFirstNonRepeatingCharacter("Maduranga");
                Console.WriteLine(ch != '\0' ? $"First non-repeating: {ch}" : "None found");
                break;
            case "7":
                RunMergeListsDemo();
                break;
            case "8":
                PracticeDemos.PrintRecordIllustrations();
                PracticeDemos.PrintTupleIllustrations();
                break;
            case "9":
                PracticeDemos.PrintPatternMatchingStatements();
                break;
            case "10":
                PracticeDemos.PrintSingletonPatternFunctions();
                break;
            case "11":
                PracticeDemos.PrintBuilderResults();
                break;
            case "12":
                PracticeDemos.ExecuteStrategy();
                PracticeDemos.ExecuteFactory();
                break;
            case "13":
                PracticeDemos.PrintAutomapperFunctions();
                break;
            case "14":
                PracticeDemos.PrintStringManipulations();
                break;
            case "15":
                RunExtensionMethodsDemo();
                break;
            case "16":
                RunDelegatesDemo();
                break;
            case "17":
                await PracticeDemos.PrintDocumentProcessedResults();
                break;
            case "18":
                RunTypeComparisonDemo();
                break;
            default:
                Console.WriteLine("Unknown option. Try again.");
                break;
        }
    }

    private static void RunSecondLargestDemo()
    {
        int[] numbers = [15, 140, 895, 445, 3, 20, 25];
        int result = AlgorithmExercises.GetSecondLargestNumber(numbers);
        if (result != int.MinValue)
            Console.WriteLine($"Second largest: {result}");
    }

    private static void RunMergeListsDemo()
    {
        ListNode l1 = new(1, new ListNode(3, new ListNode(5)));
        ListNode l2 = new(2, new ListNode(4, new ListNode(6)));
        var merged = AlgorithmExercises.MergeTwoLists(l1, l2);
        AlgorithmExercises.PrintList(merged);
    }

    private static void RunExtensionMethodsDemo()
    {
        var colors = new List<string> { "Purple", "Black", "Blue", "Orange" };
        colors.ShowItems();
        Console.WriteLine(colors.GetRandomElement());
        colors.Shuffle().ShowItems();
    }

    private static void RunDelegatesDemo()
    {
        MyDelegate f1 = Composable.Func1;
        MyDelegate f2 = Composable.Func2;
        int a = 10, b = 15;
        f1(a, ref b);
        f2(a, ref b);
        MyDelegate chained = f1 + f2;
        chained(a, ref b);
    }

    private static void RunTypeComparisonDemo()
    {
        var s = new Models.Samples.SampleStruct { a = 10, b = true, value = "original" };
        PracticeDemos.PrintStruct(s);
        Console.WriteLine($"After demo — struct: {s.a}, {s.b}, {s.value}");

        var c = new Models.Samples.SampleClass { a = 10, b = true, value = "original" };
        PracticeDemos.PrintClass(c);
        Console.WriteLine($"After demo — class: {c.a}, {c.b}, {c.value}");
    }
}
