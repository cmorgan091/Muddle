using System;
using System.Collections.Generic;
using System.Linq;

namespace Muddle.Domain.Generators.DepthFirst
{
    public static class DfMapGeneratorExtensions
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
 
        public static DfMapGenerator.CellState OppositeWall(this DfMapGenerator.CellState orig)
        {
            return (DfMapGenerator.CellState)(((int) orig >> 2) | ((int) orig << 2)) & DfMapGenerator.CellState.Initial;
        }
 
        public static bool HasFlag(this DfMapGenerator.CellState cs, DfMapGenerator.CellState flag)
        {
            return ((int)cs & (int)flag) != 0;
        }
    }
}