namespace Practice;

public class Program
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
