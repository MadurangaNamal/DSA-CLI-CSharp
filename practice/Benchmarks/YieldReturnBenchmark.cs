using BenchmarkDotNet.Attributes;
using Practice.Models;

namespace Practice.Benchmarks;

[MemoryDiagnoser]
public class YieldReturnBenchmark
{
    [Benchmark]
    public void ProcessStudents()
    {
        var studentsList = GetStudents(1_000_000);

        foreach (var student in studentsList)
        {
            if (student.Id < 1000)
                Console.WriteLine($"Person Name: {student.Name}");
            else
                break;
        }
    }

    private IEnumerable<Pupil> GetStudents(int count)
    {
        List<Pupil> students = new();

        for (int i = 0; i < count; i++)
        {
            students.Add(new Pupil { Id = i, Name = $"Test {i}" });
        }

        return students;
    }

    [Benchmark]
    public void ProcessStudentsYield()
    {
        var students = GetStudentsYield(1_000_000);

        foreach (var student in students)
        {
            if (student.Id < 1000)
                Console.WriteLine($"Id: {student.Id}, Name: {student.Name}");
            else
                break;
        }
    }

    private IEnumerable<Pupil> GetStudentsYield(int count)
    {
        for (int i = 0; i < count; i++)
        {
            yield return new Pupil() { Id = i, Name = $"Name {i}" };
        }
    }
}
