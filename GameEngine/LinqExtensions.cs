using System;
using System.Collections.Generic;

namespace GameEngine
{
    public static class LinqExtensions
    {
        public static T RemoveMin<T>(this ICollection<T> collection, Func<T, int> valueFunction)
        {
            T minimumElement = default;
            var minimumValue = int.MaxValue;
            foreach (var element in collection)
            {
                var value = valueFunction(element);
                if (minimumValue > value)
                {
                    minimumValue = value;
                    minimumElement = element;
                }
            }
            collection.Remove(minimumElement);
            return minimumElement;
        }
    }
}
