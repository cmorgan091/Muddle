using System;
using System.Collections.Generic;
using System.Linq;

namespace Muddle.Domain.Generators.DepthFirst
{
    public static class MapGeneratorExtensions
    {
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random rng)
        {
            var e = source.ToArray();
            for (var i = e.Length - 1; i >= 0; i--)
            {
                var swapIndex = rng.Next(i + 1);
                yield return e[swapIndex];
                e[swapIndex] = e[i];
            }
        }
 
        public static MapGenerator.CellState OppositeWall(this MapGenerator.CellState orig)
        {
            return (MapGenerator.CellState)(((int) orig >> 2) | ((int) orig << 2)) & MapGenerator.CellState.Initial;
        }
 
        public static bool HasFlag(this MapGenerator.CellState cs, MapGenerator.CellState flag)
        {
            return ((int)cs & (int)flag) != 0;
        }
    }
}