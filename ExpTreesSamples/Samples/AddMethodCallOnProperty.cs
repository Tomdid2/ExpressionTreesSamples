using System;
using System.Linq.Expressions;
using System.Reflection;
using ExpTreesSamples.Samples.Entities;

namespace ExpTreesSamples.Samples
{
   public class AddMethodCallOnProperty : ISample
   {
       public string Description => "Wizytator: Dodanie wywołania metody na propercji";

        public void Run()
        {
            Expression<Func<Person, bool>> expr = s => s.Name == "Anna" && s.Age > 18;

            ExpressionInspector.WriteExpression(expr);

            ConsoleWrapper.WriteLineInColor(ConsoleColor.White, "Uruchomienie wizytatora na wyrażeniu:\n\r");

            var newExpr = new StringTrimVisitor().Visit(expr);

            ExpressionInspector.WriteExpression(newExpr);
        }
    }

    public class StringTrimVisitor : ExpressionVisitor
    {
        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.Type == typeof(string))
            {
                MethodInfo trimMethod = typeof(string).GetMethod(nameof(string.Trim), new Type[0]);
                Expression trimMethodCall = Expression.Call(node, trimMethod);
                return trimMethodCall;
            }

            return node;
        }
    }
}
