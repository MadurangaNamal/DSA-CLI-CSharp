using System.Text;

namespace practice;

public class Program
{
    static void Main(string[] args)
    {

        #region Delegates exercise1
        //MyDelegate func = Composable.Func1;
        //MyDelegate func2 = Composable.Func2;

        //int a = 10;
        //int b = 15;

        //func(a, ref b);
        //func2(a, ref b);

        //Console.WriteLine("Calling the chained delegates");
        //MyDelegate del = func + func2;
        //del(a, ref b);
        #endregion

        #region Find Second largest number

        //int[] randomNumbers = { 15, 140, 895, 445, 3, 20, 25 };
        ////int[] randomNumbers = { 15, 15 };
        //int secondLargestNumber = CodePractice.GetSecondLargestNumber(randomNumbers);

        //if (secondLargestNumber != int.MinValue)
        //    Console.WriteLine("Second largest number: " + secondLargestNumber);

        #endregion

        #region Find element position in sorted array
        //int[] sortedArray = { 1, 35, 51, 78, 99, 110 };
        //int itemIndex = PracticeAlgoirthms.FindElementInSortedArray(99, sortedArray);

        //if (itemIndex != -1)
        //    Console.WriteLine("Item found in position: {0}", itemIndex + 1);
        //else
        //    Console.WriteLine("Item not found");
        #endregion

        #region Merge two sorted linked lists into one sorted list
        //ListNode l1 = new(1, new ListNode(3, new ListNode(5))); // Creating a linked list: 1 -> 3 -> 5 -> null
        //ListNode l2 = new(2, new ListNode(4, new ListNode(6))); // Creating a linked list: 2 -> 4 -> 6 -> null

        //ListNode? mergedList = PracticeAlgoirthms.MergeTwoLists(l1, l2);
        //PracticeAlgoirthms.PrintList(mergedList); // Output: 1 -> 2 -> 3 -> 4 -> 5 -> 6 -> null
        #endregion

        #region Usage: generic singly linked list
        //GenericList<int> myList = new();
        //myList.AddHead(1);
        //myList.AddHead(2);
        //myList.AddHead(3);

        //foreach (var item in myList)
        //{
        //    Console.WriteLine(item);
        //}
        #endregion

        #region class vs struct
        /*
            The reason for this is, the function receives a copy of the struct(because its a value type), so original
            keeps unchanged, but in case of class, the function receives the reference of the object(reference type)
         */

        //SampleStruct s1 = new();
        //s1.a = 10;
        //s1.b = true;
        //s1.value = "Not changed";
        //PrintStruct(s1);

        //Console.WriteLine($"Struct values {s1.a} , {s1.b}, {s1.value}");

        //SampleClass s2 = new();
        //s2.a = 10;
        //s2.b = true;
        //s2.value = "Not changed";
        //PrintClass(s2);

        //Console.WriteLine($"Class values {s2.a} , {s2.b}, {s2.value}");
        #endregion

        #region Task Progress Reporting

        //Console.WriteLine("Task Progressing...");

        //string[] departments = { "HR", "IT", "Finance", "Admin", "Sales" };

        //IProgress<int> progressReporter = new Progress<int>(value => Console.WriteLine($"Processing Department: {value}"));

        //Task.Run(async () =>
        //{
        //    await CodePractice.ProcessDataAsync(departments, progressReporter);
        //}).Wait();

        #endregion

        #region Using inbuilt Linked list

        //string[] names = { "John Cena", "John Doe", "Jane Eyere", "Doe Little" };
        //CodePractice.PrintLinkedListFunctions(names);

        #endregion

        #region String Builder

        // Create a StringBuilder that expects to hold 50 characters.
        // Initialize the StringBuilder with "ABC".
        //StringBuilder sb = new("ABC", 50);
        //Console.WriteLine($"Capacity: {sb.Capacity}, MaxCapacity: {sb.MaxCapacity}");

        //// Append three characters (D, E, and F) to the end of the StringBuilder.
        //sb.Append(new char[] { 'D', 'E', 'F' });

        //// Append a format string to the end of the StringBuilder.
        //sb.AppendFormat("GHI{0}{1}", 'J', 'k');

        //// Display the number of characters in the StringBuilder and its string.
        //Console.WriteLine($"{sb.Length} chars: {sb.ToString()}");

        //// Use AppendJoin to add an array of strings with a separator character
        //sb.AppendJoin("-", new char[] { 'L', 'M', 'N', 'O' });

        //// Insert a string at the beginning of the StringBuilder.
        //sb.Insert(0, "Alphabet: ");

        //// Replace all lowercase k's with uppercase K's.
        //sb.Replace('k', 'K');

        //// Display the number of characters in the StringBuilder and its string.
        //Console.WriteLine($"{sb.Length} chars: {sb.ToString()}");

        #endregion

        #region Pass by reference vs Pass by value

        //int number = 5;
        //Console.WriteLine($"Value before calling the Square function: {number}");
        //Square(number);
        //Console.WriteLine($"Value after calling the Square function: {number}");

        //SquareRef(ref number);
        //Console.WriteLine($"Value after calling the SquareRef function: {number}");

        #endregion

        #region Reverse any string

        //var name = "AGNARUDAM";
        //Console.WriteLine($"Reversed text: {ReverseString(name)}");

        #endregion

        #region Practicing Linq Queries

        //int[] idValues = { 41, 42, 43, 44, 45, 46, 47, 48, 49, 50 };
        //List<Employee> filteredEmplyees = CodePractice.GetDepartmentStats();
        /*var departmentStatus = CodePractice.GetDepartmentStats();

        foreach (var status in departmentStatus)
        {
            //Console.WriteLine($"{employee.Id}, {employee.Name}, {employee.Department}, {employee.Salary}, {employee.JoinDate}");
            Console.WriteLine(status);
        }*/

        #endregion

        #region Using AutoMapper for mapping objects

        //CodePractice.PrintAutomapperFunctions();
        #endregion

        #region Extension Methods

        //var colors = new List<string> { "Purple", "Black", "Blue", "Orange" };
        //colors.ShowItems();

        //var numbers = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        //numbers.ShowItems();

        //if (!colors.IsItEmpty())
        //    Console.WriteLine($"{nameof(colors)} collection has items");

        //Console.WriteLine(colors.GetRandomElement());
        //colors.Shuffle().ShowItems();

        #endregion

        #region Leetcode challenges

        ////Can jump
        //int[] numbs = new[] { 2, 3, 1, 1, 4 };
        //Console.WriteLine($"Can jump:{LeetCode.CanJump(numbs)} ");

        ////Minimum Jumps Count 
        //Console.WriteLine($"Minimum jumps:{LeetCode.CountMinJumps(numbs)}");




        #endregion

    }

    static void PrintValues()
    {
        string value = "abcd";
        ChangeValue(value);

        Console.WriteLine(value);
    }

    static void ChangeValue(string value)
    {
        value = "null";
    }

    static void SquareRef(ref int n)
    {
        n = n * n;
        Console.WriteLine($"Value inside the SquareRef function: {n}");
    }

    static void Square(int n)
    {
        n = n * n;
        Console.WriteLine($"Value inside the Square function: {n}");
    }

    //helper methods
    static void PrintStruct(SampleStruct theStruct)
    {
        theStruct.a = 20;
        theStruct.b = false;
        theStruct.value = "Changed";

        Console.WriteLine($"Struct values {theStruct.a} , {theStruct.b}, {theStruct.value}");
    }

    static void PrintClass(SampleClass theClass)
    {
        theClass.a = 20;
        theClass.b = false;
        theClass.value = null;

        Console.WriteLine($"Class values {theClass.a} , {theClass.b}, {theClass.value}");
    }

    static string ReverseString(string text)
    {
        StringBuilder reversedText = new("");

        if (!string.IsNullOrEmpty(text))
        {
            char[] characters = text.ToCharArray();

            for (int i = characters.Length - 1; i >= 0; i--)
            {
                reversedText.Append(characters[i]);
            }
        }

        return reversedText.ToString();
    }
}

class SampleClass // reference type
{
    public int a;
    public bool b;
    public string value;
}

struct SampleStruct // value type
{
    public int a;
    public bool b;
    public string value;
}

