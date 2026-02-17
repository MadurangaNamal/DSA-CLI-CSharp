using System.Collections.Concurrent;

namespace practice.practiceFiles;

public static class DocumentProcessor
{
    public static async Task<IReadOnlyDictionary<string, int>> ProcessFilesAsync(string[] filePaths, CancellationToken cancellationToken)
    {
        var wordCounts = new ConcurrentDictionary<string, int>();

        await Parallel.ForEachAsync(filePaths, cancellationToken, async (filePath, ct) =>
        {
            try
            {
                string content = await File.ReadAllTextAsync(filePath, ct);
                var words = content.Split(new[] { ' ', '\n', '\r', '\t', '.', ',', '!', '?', ';', ':', '"', '\'', '(', ')', '[', ']', '{', '}' },
                    StringSplitOptions.RemoveEmptyEntries);

                foreach (var word in words)
                {
                    wordCounts.AddOrUpdate(word.ToLowerInvariant(), 1, (_, oldValue) => oldValue + 1);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error processing file {filePath}", ex);
            }
        });

        return wordCounts;
    }
}
