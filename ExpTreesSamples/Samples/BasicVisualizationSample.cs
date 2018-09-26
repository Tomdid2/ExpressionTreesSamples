using System;
using System.Linq.Expressions;
using ExpTreesSamples.Samples.Entities;

namespace ExpTreesSamples.Samples
{
    public class BasicVisualizationSample : ISample
    {
        public string Description => @"Wizualizacja prostego drzewa";

        public void Run()
        {
            Expression<Func<int, Person, bool>> expression =
                (x, person) => person.Name.Length > x || person.Age >= 18;

            ExpressionInspector.WriteExpression(expression);
        }
    }
}
