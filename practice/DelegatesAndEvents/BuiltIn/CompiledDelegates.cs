namespace Practice.DelegatesAndEvents.BuiltIn;

/// <summary>
/// This class demonstrates the use of built-in delegates in C# such as Func, Action, Predicate, Comparison, and Converter. 
/// It shows how to define and invoke these delegates with a sample class.
/// </summary>
public static class CompiledDelegates
{
    static Func<MySampleClass, string> func = c => c.MyMethod(5, "maduranga");
    public static void Execute()
    {
        MySampleClass obj = new();

        // Func<T, TResult>  →  TAKES input, RETURNS a value
        string r1 = func(obj);
        Console.WriteLine($"Func      -> returns : {r1}");

        // Action<T>  →  TAKES input, RETURNS NOTHING (void)
        Action<MySampleClass> action = c => Console.WriteLine($"Action    -> returns : nothing (printed {c.MyMethod(5, "maduranga")})");
        action(obj);

        // Predicate<T>  →  TAKES input, RETURNS bool
        Predicate<MySampleClass> predicate = c => c.MyProperty;
        bool r2 = predicate(obj);
        Console.WriteLine($"Predicate -> returns : {r2}");

        // Comparison<T>  →  TAKES TWO inputs, RETURNS int (for sorting)
        Comparison<MySampleClass> comparison = (a, b) => a.MyProperty.CompareTo(b.MyProperty);
        int r3 = comparison(obj, new MySampleClass { MyProperty = false });
        Console.WriteLine($"Comparison-> returns : {r3}  (-1, 0, or 1)");

        // Converter<TIn, TOut>  →  TAKES input, RETURNS a DIFFERENT type
        Converter<MySampleClass, string> converter = c => c.MyMethod(5, "maduranga");
        string r4 = converter(obj);
        Console.WriteLine($"Converter -> returns : {r4}");
    }
}

public class MySampleClass
{
    public bool MyProperty { get; set; } = true;
    public string MyMethod(int number, string name) => number + name;
}
