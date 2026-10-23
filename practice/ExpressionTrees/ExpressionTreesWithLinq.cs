using Microsoft.EntityFrameworkCore;
using Practice.Data;
using Practice.EfCore.Models;
using System.Linq.Expressions;

namespace Practice.ExpressionTrees;

public class ExpressionTreesWithLinq
{
    private readonly ApplicationDbContext _dbContext;

    public ExpressionTreesWithLinq()
    {
        _dbContext = new();
    }

    public async Task ExecuteAsync()
    {
        var result = await GetSeniorORNewEmployees();

        Console.WriteLine();
        foreach (var e in result)
            Console.WriteLine($"{e.FirstName} {e.LastName} — {e.JoinedDate:yyyy-MM-dd}");
    }

    private async Task<List<Employee>> GetSeniorORNewEmployees()
    {
        Expression<Func<Employee, bool>> joinedBefore2022 =
             e => e.JoinedDate < new DateTime(2022, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        Expression<Func<Employee, bool>> joinedAfter2023 =
            e => e.JoinedDate > new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        var combined = ExpressionTreesWithLinq.Combine(joinedBefore2022, joinedAfter2023);

        return await _dbContext.Employees
            .Where(combined)
            .ToListAsync();
    }

    public static Expression<Func<T, bool>> Combine<T>(
        Expression<Func<T, bool>> left,
        Expression<Func<T, bool>> right,
        ExpressionType combineType = ExpressionType.OrElse)
    {
        // Create a new shared parameter
        var param = Expression.Parameter(typeof(T), "x");

        // Replace the parameters in both bodies with the shared one
        var leftBody = new ParameterReplacer(left.Parameters[0], param).Visit(left.Body);
        var rightBody = new ParameterReplacer(right.Parameters[0], param).Visit(right.Body);

        // Combine
        var body = Expression.MakeBinary(combineType, leftBody, rightBody);
        return Expression.Lambda<Func<T, bool>>(body, param);
    }

}

internal class ParameterReplacer : ExpressionVisitor
{
    private readonly ParameterExpression _oldParam;
    private readonly ParameterExpression _newParam;
    public ParameterReplacer(ParameterExpression oldParam, ParameterExpression newParam)
        => (_oldParam, _newParam) = (oldParam, newParam);

    protected override Expression VisitParameter(ParameterExpression node)
        => node == _oldParam ? _newParam : base.VisitParameter(node);
}
