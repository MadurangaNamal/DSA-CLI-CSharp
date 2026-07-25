namespace Practice;

public static class Program
{
    static async Task Main(string[] args)
    {
        if (args.Length > 0)
        {
            await PracticeRunner.RunChoiceFromArgsAsync(args[0].Trim());
            return;
        }

        await PracticeRunner.RunAsync();
    }
}
