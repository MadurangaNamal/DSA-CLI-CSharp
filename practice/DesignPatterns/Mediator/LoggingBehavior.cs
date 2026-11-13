using MediatR;
using System.Diagnostics;

namespace Practice.DesignPatterns.Mediator;

/// <summary>
/// Custom logging behavior for MediatR pipeline.
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken ct)
    {
        var name = typeof(TRequest).Name;
        var kind = name.EndsWith("Query") ? "QUERY" : "COMMAND";
        var sw = Stopwatch.StartNew();

        Console.WriteLine($"[{kind}] -> {name}");

        var response = await next(ct);

        Console.WriteLine($"[{kind}] <- {name} ({sw.ElapsedMilliseconds} ms)");

        return response;
    }
}
