using System;
using System.Linq.Expressions;

namespace ExpTreesSamples
{
    public static class ExpressionInspector
    {
        public static void WriteExpression(Expression expression)
        {
            var preserveColor = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(expression);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            WriteExpressionInner(expression, "", "");
            Console.WriteLine();
            Console.ForegroundColor = preserveColor;
        }

        public static void WriteExpressionInner(Expression expression, string indent, string label)
        {
            if (expression == null)
            {
                return;
            }

            switch (expression.NodeType)
            {
                case ExpressionType.Add:
                case ExpressionType.AddChecked:
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                case ExpressionType.ArrayIndex:
                case ExpressionType.Coalesce:
                case ExpressionType.Divide:
                case ExpressionType.Equal:
                case ExpressionType.ExclusiveOr:
                case ExpressionType.GreaterThan:
                case ExpressionType.GreaterThanOrEqual:
                case ExpressionType.LeftShift:
                case ExpressionType.LessThan:
                case ExpressionType.LessThanOrEqual:
                case ExpressionType.Modulo:
                case ExpressionType.Multiply:
                case ExpressionType.MultiplyChecked:
                case ExpressionType.NotEqual:
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                case ExpressionType.Power:
                case ExpressionType.RightShift:
                case ExpressionType.Subtract:
                case ExpressionType.SubtractChecked:

                    var binaryExpression = (BinaryExpression)expression;

                    Console.WriteLine(indent + label + " {0}:{1}{2}{3} (",
                        typeof(BinaryExpression).Name,
                        binaryExpression.NodeType,
                        binaryExpression.IsLifted ? ", IsLifted" : "",
                        binaryExpression.IsLiftedToNull ? ", IsLiftedToNull" : "");

                    WriteExpressionInner(binaryExpression.Left, "   " + indent, " Left: ");
                    WriteExpressionInner(binaryExpression.Right, "   " + indent, " Right: ");
                    WriteExpressionInner(binaryExpression.Conversion, "   " + indent, " Conversion: ");

                    break;
                case ExpressionType.ArrayLength:
                case ExpressionType.Convert:
                case ExpressionType.ConvertChecked:
                case ExpressionType.Negate:
                case ExpressionType.UnaryPlus:
                case ExpressionType.NegateChecked:
                case ExpressionType.Not:
                case ExpressionType.Quote:
                case ExpressionType.TypeAs:

                    var unaryExpression = (UnaryExpression)expression;

                    Console.WriteLine(indent + label + " {0} :{1}{2}{3} (",
                        typeof(BinaryExpression).Name,
                        unaryExpression.NodeType,
                        unaryExpression.IsLifted ? ", IsLifted" : "",
                        unaryExpression.IsLiftedToNull ? ", IsLiftedToNull" : "");

                    WriteExpressionInner(unaryExpression.Operand, "   " + indent, " Operand: ");

                    break;

                case ExpressionType.Call:

                    var methodCallExpression = (MethodCallExpression)expression;

                    Console.WriteLine($"{indent}{label} {typeof(MethodCallExpression).Name} :");

                    foreach (var item in methodCallExpression.Arguments)
                    {
                        WriteExpressionInner(item, "   " + indent, "-> Arguments:");
                    }

                    WriteExpressionInner(methodCallExpression.Object, "   " + indent, "-> Object:");

                    break;

                case ExpressionType.Conditional:

                    var conditionalExpression = (ConditionalExpression)expression;

                    Console.WriteLine($"{indent}{label} {typeof(ConditionalExpression).Name} :");

                    WriteExpressionInner(conditionalExpression.Test, indent + "  ", "-> Test: ");

                    break;

                case ExpressionType.Constant:

                    Console.WriteLine($"{indent}{label} {typeof(ConstantExpression).Name} ({((ConstantExpression)expression).Value})");

                    break;

                case ExpressionType.Invoke:

                    var inv = (InvocationExpression)expression;

                    Console.WriteLine($"{indent}{label} {typeof(ConstantExpression).Name} :");

                    foreach (var item in inv.Arguments)
                    {
                        WriteExpressionInner(item, "   " + indent, "-> Argument:");
                    }

                    WriteExpressionInner(inv.Expression, "   " + indent, "-> Expression:");

                    break;

                case ExpressionType.Lambda:

                    var lambda = (LambdaExpression)expression;
                    Console.WriteLine($"{indent}{label} {typeof(LambdaExpression).Name}:");


                    foreach (var item in lambda.Parameters)
                    {
                        WriteExpressionInner(item, "   " + indent, "-> Parameter: ");
                    }

                    WriteExpressionInner(lambda.Body, "   " + indent, "-> Body: ");

                    break;
                case ExpressionType.Parameter:
                    Console.WriteLine($"{indent}{label} {typeof(ParameterExpression).Name} ({((ParameterExpression)expression).Name})");
                    break;
                case ExpressionType.MemberAccess:
                    Console.WriteLine($"{indent}{label} {typeof(MemberExpression).Name} ({{0}})", expression);
                    break;
                case ExpressionType.ListInit:
                case ExpressionType.MemberInit:
                case ExpressionType.New:
                case ExpressionType.NewArrayInit:
                case ExpressionType.NewArrayBounds:
                case ExpressionType.TypeIs:
                    break;
            }
        }
    }
}
