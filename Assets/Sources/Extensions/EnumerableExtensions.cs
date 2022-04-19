using System.Collections.Generic;
using System.Linq;

namespace Sources.Extensions
{
    public static class EnumerableExtensions
    {
        public static T Anyone<T>(this IEnumerable<T> enumerable) =>
            enumerable.ElementAt(UnityEngine.Random.Range(0, enumerable.Count()));
    }
}