using Practice.DelegatesAndEvents.BuiltIn;
using System.Linq.Expressions;

namespace Practice.ExpressionTrees;

/// <summary>
/// The expression is parsed to identify its components, and then it is compiled and executed to obtain the result.
/// </summary>
public static class ExpressionSmpl
{
    public static void Execute()
    {
        Expression<Func<MySampleClass, string>> expr = c => c.MyMethod(19, "Maduranga");
        Expression<Func<MySampleClass, bool>> propExpr = c => c.MyProperty;

        ParseExpression(expr);

        var exprFunction = expr.Compile(); // Compiles the expression into a delegate that can be invoked at runtime(Expensive operation)
        var result = exprFunction(new MySampleClass());
        Console.WriteLine("\n" + result);
        Console.WriteLine(propExpr.Compile()(new()));
    }

    private static void ParseExpression(Expression expression)
    {
        if (expression.NodeType == ExpressionType.Lambda)
        {
            var lambdaExpression = (LambdaExpression)expression;

            Console.WriteLine("Lambda");

            // Recurse into the lambda body
            ParseExpression(lambdaExpression.Body);
        }
        else if (expression.NodeType == ExpressionType.Call)
        {
            var methodCallExpression = (MethodCallExpression)expression;

            Console.WriteLine("Method");
            Console.WriteLine(methodCallExpression.Method.Name);

            // Recurse into the object the method is called on (c.MyMethod -> c.MyMethod)
            if (methodCallExpression.Object != null)
            {
                ParseExpression(methodCallExpression.Object);
            }

            // Recurse into each argument
            foreach (var argument in methodCallExpression.Arguments)
            {
                ParseExpression(argument);
            }
        }
        else if (expression.NodeType == ExpressionType.MemberAccess)
        {
            var memberExpression = (MemberExpression)expression;

            Console.WriteLine("Property");
            Console.WriteLine(memberExpression.Member.Name);

            // Recurse into the expression the member is accessed on (c.MyProperty -> c)
            if (memberExpression.Expression != null)
            {
                ParseExpression(memberExpression.Expression);
            }
        }
        else if (expression.NodeType == ExpressionType.Constant)
        {
            var constantExpression = (ConstantExpression)expression;

            // .Value holds the actual constant
            Console.WriteLine(constantExpression.Value);
        }
        else if (expression.NodeType == ExpressionType.Parameter)
        {
            var parameterExpression = (ParameterExpression)expression;

            Console.WriteLine("Parameter");
            Console.WriteLine(parameterExpression.Name);
        }
    }
}
