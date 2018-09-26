using System;
using ExpTreesSamples.Samples.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Newtonsoft.Json;

namespace ExpTreesSamples.Samples
{
    public class DynamicWhereSample : ISample
    {
        public string Description => "Dynamicznie budowana klauzula Where";

        public void Run()
        {
            var collection = new List<Person>
            {
                new Person { Name ="Adam", Age = 12 },
                new Person { Name ="Baltazar", Age = 34 },
                new Person { Name ="Balbina" , Age = 17 }
            }.AsQueryable();

            var item = Expression.Parameter(typeof(Person), "item");

            var ageProperty = Expression.Property(item, nameof(Person.Age));
            var ageConstVal = Expression.Constant(18);
            var ageEqual = Expression.LessThanOrEqual(ageProperty, ageConstVal);

            var nameProperty = Expression.Property(item, nameof(Person.Name));
            var nameConstVal = Expression.Constant("B");
            MethodInfo nameStartsWith = typeof(string).GetMethod(nameof(string.StartsWith), new[] { typeof(string) });
            Expression nameStartsWithCall = Expression.Call(nameProperty, nameStartsWith, nameConstVal);

            var fullExpression = Expression.AndAlso(ageEqual, nameStartsWithCall);
            var lambda = Expression.Lambda<Func<Person, bool>>(fullExpression, item);

            var result = collection.Where(lambda).ToList();

            ConsoleWrapper.WriteLineInColor(ConsoleColor.White, "Przykładowa kolekcja:\n\r");
            ConsoleWrapper.WriteLineInColor(ConsoleColor.Cyan, $"{JsonConvert.SerializeObject(collection, Formatting.Indented)}\n\r");

            ExpressionInspector.WriteExpression(lambda);

            ConsoleWrapper.WriteLineInColor(ConsoleColor.White, "Wynik filtrowania:\n\r");
            ConsoleWrapper.WriteLineInColor(ConsoleColor.Cyan, $"{JsonConvert.SerializeObject(result, Formatting.Indented)}\n\r");
        }
    }
}
