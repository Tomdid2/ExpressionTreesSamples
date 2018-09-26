using System;
using System.Linq.Expressions;
using System.Reflection;
using ExpTreesSamples.Samples.Entities;

namespace ExpTreesSamples.Samples
{
   public class MergeTwoExpressions : ISample
   {
       public string Description => "Wizytator: Łączenie dwóch wyrażeń z różnymi parametrami";

        public void Run()
        {
            ConsoleWrapper.WriteLineInColor(ConsoleColor.White, "Posiadamy dwa wyrażenia do połączenia:\n\r");

            Expression<Func<Person, bool>> expr = s => s.Name == "Anna";
            ExpressionInspector.WriteExpression(expr);

            Expression<Func<Person, bool>> expr2 = s2 => s2.Age == 18;
            ExpressionInspector.WriteExpression(expr2);

            var param = Expression.Parameter(typeof(Person), "person");

            ConsoleWrapper.WriteLineInColor(ConsoleColor.White, "Możemy połączyć ich body, ale definicje parametrów wciąż się różnią:\n\r");

            var exprBody = Expression.Or(expr.Body, expr2.Body);

            var expressionToRewrite = Expression.Lambda<Func<Person, bool>>(exprBody, param);

            ExpressionInspector.WriteExpression(exprBody);

            ConsoleWrapper.WriteLineInColor(ConsoleColor.White, "Uruchomienie wizytatora na wyrażeniu:\n\r");

            var finalExpression = new ParameterReplacer(param).Visit(expressionToRewrite);

            ExpressionInspector.WriteExpression(finalExpression);
        }
    }

    public class ParameterReplacer : ExpressionVisitor
    {
        private readonly ParameterExpression _parameter;

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return base.VisitParameter(_parameter);
        }

        public ParameterReplacer(ParameterExpression parameter)
        {
            _parameter = parameter;
        }
    }
}
