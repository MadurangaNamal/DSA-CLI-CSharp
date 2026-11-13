# C# & SQL Practice Playground

A .NET 8 console project for learning and experimenting with C# language features, algorithms, design patterns, LINQ, threading, and SQL.

## Quick start

1. Install the [.NET 8 SDK](https://dotnet.microsoft.com/download).
2. Open `AA Todo.sln` in Visual Studio or VS Code.
3. Run the project:

```bash
cd practice
dotnet run
```

An interactive menu lists all demos. Pick a number, or quit with `Q`.

Run a single demo without the menu:

```bash
dotnet run -- 1    # custom exceptions
dotnet run -- 4    # LINQ SQL-style walkthrough
```

## Project layout

```
practice/
├── Program.cs                  # Entry point
├── PracticeRunner.cs           # Interactive menu
├── Algorithms/                 # Classic algorithms and coding challenges
├── Benchmarks/                 # BenchmarkDotNet examples
│   └── Tasks/                  # Task and ValueTask benchmark samples
├── Collections/                # Collection extension methods
├── Concurrency/                # Threading, tasks, and parallel processing
├── Data/                       # Sample data factories
├── DataStructures/             # Custom data structures
├── Database/                   # SQL client examples
├── DelegatesAndEvents/         # Delegates, events, and related demos
├── Demos/                      # Runnable demo orchestration
├── DesignPatterns/             # Singleton, builder, strategy, factory, and mediator
│   └── Mediator/               # Mediator pattern examples
├── EfCore/                     # Entity Framework Core examples
│   ├── Entities/               # EF Core entities
│   └── Repository/              # Repository implementations
├── Exceptions/                 # Custom exception types
├── ExpressionTrees/            # Expression tree and LINQ examples
├── Mapping/                    # AutoMapper profiles
├── Migrations/                 # Entity Framework Core migrations
├── Models/                     # Domain models and DTOs
│   └── Samples/                # Types used in class/struct/record comparisons
├── Samples/
│   └── Documents/              # Text files copied to build output for demos
├── Sql/                        # SQL practice scripts (run in SSMS or similar)
└── Strings/                    # String manipulation exercises
```

## Namespaces

Code is organized under the `Practice` root namespace, with folders matching sub-namespaces (for example `Practice.Algorithms`, `Practice.Demos`, `Practice.DesignPatterns`).

## What each area covers

| Folder | Topics |
|--------|--------|
| **Algorithms** | Binary search, linked-list merge, string problems, LeetCode-style challenges |
| **Demos** | LINQ queries, AutoMapper, pattern matching, design-pattern runners |
| **DesignPatterns** | Singleton, builder, strategy, factory |
| **Concurrency** | Threads, mutex/semaphore, `Parallel.ForEachAsync` |
| **Collections** | Shuffle, random element, `ShowItems` extensions |
| **Exceptions** | Validation, insufficient funds, resource-not-found, database errors |
| **Sql** | Table creation, joins, aggregations (execute outside the app) |

## SQL scripts

Open `practice/Sql/SQLQueryPractice.sql` in SQL Server Management Studio (or Azure Data Studio) and run sections as needed. The C# database demo uses a connection string in code and expects a local SQL Server instance.

## IDE configuration

VS Code launch and build tasks target `AA Todo.sln` and `practice/bin/Debug/net8.0/practice.dll`.
