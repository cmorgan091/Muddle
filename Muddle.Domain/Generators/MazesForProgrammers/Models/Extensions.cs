using System;
using System.Collections.Generic;
using System.Linq;

namespace Muddle.Domain.Generators.MazesForProgrammers.Models
{
    public static class Extensions
    {
        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            if (items == null)
            {
                return;
            }

            foreach (var item in items)
            {
                action(item);
            }
        }

        public static T Rand<T>(this IEnumerable<T> items, Random r = null) =>
            items.Shuffle(r).First();

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> items, Random r) =>
            items.OrderBy(n => r.Next());
    }
}