using System;
using Muddle.Domain.Generators.DepthFirst;
using Muddle.Domain.Generators.MazesForProgrammers;
using Muddle.Domain.Models;

namespace Muddle.Domain.Generators
{
    public class MapBuilderGenerator
    {
        public MapBuilder Generate(Algorithms algorithm, int width, int height)
        {
            var model = new MapBuilderGeneratorModel
            {
                Width = width,
                Height = height,
                Algorithm = algorithm
            };

            return Generate(model);
        }

        public MapBuilder Generate(MapBuilderGeneratorModel model)
        {
            switch (model.Algorithm)
            {
                case Algorithms.DepthFirst:
                    return new DfMapGenerator(model.Width, model.Height, model.BlockedPoints).Generate();
                default:
                    return new MfpMapGenerator().Generate(model.Width, model.Height, model.Algorithm.ToMfpAlgorithm());
            }

            throw new Exception($"No generate implementation for algorithm {model.Algorithm}");
        }
    }
}

