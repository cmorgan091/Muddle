using System;
using Muddle.Domain.Generators.MazesForProgrammers;

namespace Muddle.Domain.Generators
{
    public static class MapBuilderGeneratorHelper
    {
        public static MfpMapGenerator.Algorithms ToMfpAlgorithm(this Algorithms algorithm)
        {
            switch (algorithm)
            {
                case Algorithms.DepthFirst:
                    throw new Exception($"{algorithm} is not a Mazes for Programmers Algorithm");
                case Algorithms.AldousBroder:
                    return MfpMapGenerator.Algorithms.AldousBroder;
                case Algorithms.AldousBroderAvoidLinks:
                    return MfpMapGenerator.Algorithms.AldousBroderAvoidLinks;
                case Algorithms.AldousBroderWilson:
                    return MfpMapGenerator.Algorithms.AldousBroderWilson;
                case Algorithms.BinaryTree:
                    return MfpMapGenerator.Algorithms.BinaryTree;
                case Algorithms.Sidewinder:
                    return MfpMapGenerator.Algorithms.Sidewinder;
                case Algorithms.Wilson:
                    return MfpMapGenerator.Algorithms.Wilson;
                case Algorithms.WilsonJb:
                    return MfpMapGenerator.Algorithms.WilsonJb;
            }

            throw new Exception($"{algorithm} could not be mapped to a Mazes for Programmers Algorithm");
        }
    }
}