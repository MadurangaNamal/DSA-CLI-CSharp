using BenchmarkDotNet.Running;
using Microsoft.Extensions.Logging;
using Practice.Algorithms;
using Practice.Benchmarks;
using Practice.Collections;
using Practice.Concurrency;
using Practice.Data;
using Practice.DataStructures;
using Practice.DelegatesAndEvents;
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
        Console.WriteLine("19  Benchmarks (Need to execute in Release Mode)");
        Console.WriteLine("20. EF Core Bulk Large Data Retrieval");
        Console.WriteLine("21  LeetCode — Algorithm Q&A");
        Console.WriteLine("22  EF Core Transactions");
        Console.WriteLine("23  Generic List Implementation");
        Console.WriteLine("24  Linked List Functions");
        Console.WriteLine("25  Algorithms — Reverse a String");
        Console.WriteLine("26  Algorithms — Binary Search");
        Console.WriteLine("27  LINQ — Progress Runner");
        Console.WriteLine("28  Concurrency — Threading Examples");
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
                RunFilterAndPrintEmployees();
                break;
            case "3":
                PrintDepartmentStats();
                break;
            case "4":
                LinqQueryDemos.PrintCustomLinqQueryResults();
                break;
            case "5":
                RunSecondLargestDemo();
                break;
            case "6":
                RunFirstNonRepeatingCharacterDemo();
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
                DelegatesAndEventsDemoRunner.RunDemo();
                break;
            case "17":
                await PracticeDemos.PrintDocumentProcessedResults();
                break;
            case "18":
                RunTypeComparisonDemo();
                break;
            case "19":
                RunBenchmarkTestCodes();
                break;
            case "20":
                RunEfcoreBulkDemo();
                break;
            case "21":
                RunLeetCodeSolutions();
                break;
            case "22":
                await RunEfcoreTransactionDemo();
                break;
            case "23":
                RunGenericListDemo();
                break;
            case "24":
                LinkedListDemo.PrintLinkedListFunctions(["Maduranga", "Namal", "Wimalarathne"]);
                break;
            case "25":
                RunReverseString();
                break;
            case "26":
                RunBinarySearchDemo();
                break;
            case "27":
                await LinqQueryDemos.RunProgress();
                break;
            case "28":
                await RunCustomThreads();
                break;
            default:
                Console.WriteLine("Unknown option. Try again.");
                break;
        }
    }

    private static void PrintDepartmentStats()
    {
        var stats = LinqQueryDemos.GetDepartmentStats();
        foreach (var stat in stats)
        {
            Console.WriteLine($"{stat.Department,-15} | Avg: {stat.AvgSalary,10:C} | Count: {stat.TotalEmployees,3}");
        }
    }

    private static void RunFilterAndPrintEmployees()
    {
        var filtered = LinqQueryDemos.FilterEmployees([41, 42, 43]);
        LinqQueryDemos.PrintEmployeesList(filtered);
    }

    private static void RunReverseString()
    {
        string input = "enhtaralamiW agnarudaM";
        string reversed = SampleAlgorithms.ReverseString(input);
        Console.WriteLine($"Original: {input}");
        Console.WriteLine($"Reversed: {reversed}");
    }

    private static void RunSecondLargestDemo()
    {
        int[] numbers = [15, 140, 895, 445, 3, 20, 25];
        int result = SampleAlgorithms.GetSecondLargestNumber(numbers);
        if (result != int.MinValue)
            Console.WriteLine($"Second largest: {result}");
    }

    private static void RunMergeListsDemo()
    {
        ListNode l1 = new(1, new ListNode(3, new ListNode(5)));
        ListNode l2 = new(2, new ListNode(4, new ListNode(6)));
        var merged = SampleAlgorithms.MergeTwoLists(l1, l2);
        SampleAlgorithms.PrintList(merged);
    }

    private static void RunExtensionMethodsDemo()
    {
        var colors = new List<string> { "Purple", "Black", "Blue", "Orange" };
        colors.ShowItems();
        Console.WriteLine(colors.GetRandomElement());
        colors.Shuffle().ShowItems();
    }

    private static void RunGenericListDemo()
    {
        GenericListUsage.Run();
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

    private static void RunBenchmarkTestCodes()
    {
        //_ = BenchmarkRunner.Run<YieldReturnBenchmark>();
        //_ = BenchmarkRunner.Run<BenchmarkAsyncTaskVsValueTask>();
        //_ = BenchmarkRunner.Run<StringBenchmark>();
        //_ = BenchmarkRunner.Run<AnyVsCountBenchmark>();
        //_ = BenchmarkRunner.Run<ListVsHashsetSearchBenchmark>();
        _ = BenchmarkRunner.Run<ClassVsStructBenchmark>();
    }

    private static void RunEfcoreBulkDemo()
    {
        var demo = new EfCoreBulkDemo();
        _ = demo.RunAsync();
    }

    private async static Task RunEfcoreTransactionDemo()
    {
        var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
            builder.SetMinimumLevel(LogLevel.Information);
        });

        var demo = new EfCoreTransactions(new ApplicationDbContext(), loggerFactory.CreateLogger<EfCoreTransactions>());
        await demo.RunAsync();
    }

    private static void RunFirstNonRepeatingCharacterDemo()
    {
        var ch = SampleAlgorithms.GetFirstNonRepeatingCharacter("Maduranga");
        Console.WriteLine(ch != '\0' ? $"First non-repeating: {ch}" : "None found");
    }

    private static void RunBinarySearchDemo()
    {
        int[] sortedArray = [1, 3, 5, 7, 9, 11];
        int target = 9;
        int index = SampleAlgorithms.FindElementInSortedArray(target, sortedArray);

        Console.WriteLine(index != -1 ? $"Found {target} at index {index}" : $"{target} not found");
    }
    private static void RunLeetCodeSolutions()
    {
        Console.WriteLine("\nRunning all LeetCodeSolutions examples...\n");

        void SafeRun(string name, Action action)
        {
            try
            {
                Console.WriteLine($"--- {name} ---");
                action();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in {name}: {ex.Message}");
            }
            Console.WriteLine();
        }

        SafeRun("ReverseString", () =>
            Console.WriteLine(LeetCodeSolutions.ReverseString("hello")));

        SafeRun("IsAPalindrome", () =>
            Console.WriteLine(LeetCodeSolutions.IsAPalindrome("madam")));

        SafeRun("Factorial", () =>
            Console.WriteLine(LeetCodeSolutions.Factorial(6)));

        SafeRun("PrintFibonacciSeries", () =>
        {
            Console.Write("Fibonacci: ");
            LeetCodeSolutions.PrintFibonacciSeries(7);
            Console.WriteLine();
        });

        SafeRun("FindMaximumNumber", () =>
            Console.WriteLine(LeetCodeSolutions.FindMaximumNumber(new int[] { 15, 140, 895, 445, 3, 20, 25 })));

        SafeRun("IsPrimeNumber", () =>
            Console.WriteLine(LeetCodeSolutions.IsPrimeNumber(17)));

        SafeRun("CountOccurrences", () =>
            LeetCodeSolutions.CountOccurrences("abracadabra"));

        SafeRun("RemoveDuplicateNumbers", () =>
            Console.WriteLine(string.Join(", ", LeetCodeSolutions.RemoveDuplicateNumbers(new int[] { 1, 2, 2, 3, 3, 4 }))));

        SafeRun("SwapNumbers", () =>
            LeetCodeSolutions.SwapNumbers(3, 5));

        SafeRun("FirstNonRepeatingCharacter", () =>
            Console.WriteLine(LeetCodeSolutions.FirstNonRepeatingCharacter("swiss")));

        SafeRun("MergeSortedArrays", () =>
            LeetCodeSolutions.MergeSortedArrays(new int[] { 1, 3, 5 }, 3, new int[] { 2, 4 }, 2));

        SafeRun("RemoveElement", () =>
            Console.WriteLine(LeetCodeSolutions.RemoveElement(new int[] { 3, 2, 2, 3 }, 3)));

        SafeRun("RemoveDuplicates", () =>
            Console.WriteLine(LeetCodeSolutions.RemoveDuplicates(new int[] { 1, 1, 2 })));

        SafeRun("RemoveDuplicatesV2", () =>
            Console.WriteLine(LeetCodeSolutions.RemoveDuplicatesV2(new int[] { 0, 0, 1, 1, 1, 2, 2, 3, 3, 4 })));

        SafeRun("MajorityElement", () =>
            Console.WriteLine(LeetCodeSolutions.MajorityElement(new int[] { 3, 2, 3 })));

        SafeRun("Rotate", () =>
            LeetCodeSolutions.Rotate(new int[] { 1, 2, 3, 4, 5 }, 2));

        SafeRun("MaxProfit", () =>
            Console.WriteLine(LeetCodeSolutions.MaxProfit(new int[] { 7, 1, 5, 3, 6, 4 })));

        SafeRun("MaxProfitV2", () =>
            Console.WriteLine(LeetCodeSolutions.MaxProfitV2(new int[] { 7, 1, 5, 3, 6, 4 })));

        SafeRun("MaxProfitQ2", () =>
            Console.WriteLine(LeetCodeSolutions.MaxProfitQ2(new int[] { 7, 1, 5, 3, 6, 4 })));

        SafeRun("CanJump", () =>
            Console.WriteLine(LeetCodeSolutions.CanJump(new int[] { 2, 3, 1, 1, 4 })));

        SafeRun("CountMinJumps", () =>
            Console.WriteLine(LeetCodeSolutions.CountMinJumps(new int[] { 2, 3, 1, 1, 4 })));

        SafeRun("ProductExceptSelf", () =>
            Console.WriteLine(string.Join(", ", LeetCodeSolutions.ProductExceptSelf(new int[] { 1, 2, 3, 4 }))));

        SafeRun("CanCompleteCircuit", () =>
            Console.WriteLine(LeetCodeSolutions.CanCompleteCircuit(new int[] { 1, 2, 3, 4, 5 }, new int[] { 3, 4, 5, 1, 2 })));

        SafeRun("RomanToInteger", () =>
            Console.WriteLine(LeetCodeSolutions.RomanToInteger("MCMXCIV")));

        SafeRun("IsValidString", () =>
            Console.WriteLine(LeetCodeSolutions.IsValidString("()[]{}")));

        SafeRun("ContainsDuplicate", () =>
            Console.WriteLine(LeetCodeSolutions.ContainsDuplicate(new int[] { 1, 2, 3, 1 })));

        SafeRun("IsAnagram", () =>
            Console.WriteLine(LeetCodeSolutions.IsAnagram("anagram", "nagaram")));

        SafeRun("CompressWord", () =>
            Console.WriteLine(LeetCodeSolutions.CompressWord("deeedbbcccbdaa", 3)));

        Console.WriteLine("Completed running LeetCodeSolutions examples.");
    }

    private static async Task RunCustomThreads()
    {
        Console.WriteLine("\n=== Running All Threading Examples ===\n");

        try
        {
            Console.WriteLine("1. Running ThreadMethod...");
            CustomThreadsDemo.ThreadMethod();
            Console.WriteLine("\n");

            Console.WriteLine("2. Running ParameterizedThreadMethod...");
            CustomThreadsDemo.ParameterizedThreadMethod();
            Thread.Sleep(2000); // Wait for thread to complete
            Console.WriteLine("\n");

            Console.WriteLine("3. Running ThreadSynchronization...");
            CustomThreadsDemo.ThreadSynchronization();
            Console.WriteLine("\n");

            Console.WriteLine("4. Running ThreadSafetyUsingMutex...");
            CustomThreadsDemo.ThreadSafetyUsingMutex();
            Console.WriteLine("\n");

            Console.WriteLine("5. Running ThreadSafetyUsingSemaphore...");
            CustomThreadsDemo.ThreadSafetyUsingSemaphore();
            Thread.Sleep(25000); // Wait for all threads to complete
            Console.WriteLine("\n");

            Console.WriteLine("6. Running ThreadPoolExecution...");
            CustomThreadsDemo.ThreadPoolExecution();
            Console.WriteLine("\n");

            Console.WriteLine("7. Running BackgroundWorkWithTasks...");
            CustomThreadsDemo.BackgroundWorkWithTasks();
            Console.WriteLine("\n");

            Console.WriteLine("8. Running NonBlockingOperationsWithAsyncAwait...");
            await CustomThreadsDemo.NonBlockingOperationsWithAsyncAwait();
            Console.WriteLine("\n");

            Console.WriteLine("9. Running CancellationOfTasks...");
            CustomThreadsDemo.CancellationOfTasks();
            Console.WriteLine("\n");

            Console.WriteLine("10. Running ParallelProcessing...");
            CustomThreadsDemo.ParallelProcessing();
            Console.WriteLine("\n");

            Console.WriteLine("=== All Threading Examples Completed ===");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error running examples: {ex.Message}");
        }
    }
}
