using Muddle.Domain.Generators.DepthFirst;
using Muddle.Domain.Models;

namespace Muddle.Web.Maps
{
    public static class SampleMapBuilders
    {
        public static MapBuilder SimpleMapBuilder()
        {
            var builder = new MapBuilder(8, 7);

            builder.Named("Simple Starter")
                .AddPath(1, 1, Directions.East, 5)
                .AddPath(3, 1, Directions.South, 5)
                .AddPath(3, 5, Directions.East, 5)
                .AddStart(2, 1)
                .AddEnd(5, 5);

            return builder;
        }

        public static MapBuilder MazeMapBuilder()
        {
            var builder = new MapBuilder(10, 10)
                .Named("Maze")
                .AddStart(5, 5)
                .AddEnd(1, 1)
                .AddPath(4, 5, Directions.East, 3)
                .AddPath(6, 3, Directions.South, 4)
                .AddPath(3, 3, Directions.East, 6)
                .AddPath(5, 3, Directions.South, 2)
                .AddPath(7, 3, Directions.North, 2)
                .AddPath(5, 2, Directions.East, 5)
                .AddPath(9, 0, Directions.South, 5)
                .AddPath(8, 3, Directions.South, 5)
                .AddPath(9, 7, Directions.West, 5)
                .AddPath(5, 7, Directions.North, 2)
                .AddPath(5, 6, Directions.West, 4)
                .AddPath(3, 1, Directions.South, 5)
                .AddPath(7, 7, Directions.South, 3)
                .AddPath(7, 8, Directions.West, 4)
                .AddPath(7, 9, Directions.West, 8)
                .AddPath(4, 8, Directions.North, 2)
                .AddPath(3, 9, Directions.North, 2)
                .AddPath(3, 8, Directions.West, 3)
                .AddPath(0, 9, Directions.North, 2)
                .AddPath(1, 8, Directions.North, 4)
                .AddPath(1, 7, Directions.West, 2)
                .AddPath(0, 7, Directions.North, 4)
                .AddPath(2, 6, Directions.North, 4)
                .AddPath(0, 4, Directions.East, 2)
                .AddPath(1, 4, Directions.North, 4)
                .AddPath(6, 2, Directions.North, 3)
                .AddPath(6, 0, Directions.West, 6);

            return builder;
        }

        public static MapBuilder ShroudedMazeMapBuilder()
        {
            var builder = MazeMapBuilder()
                .Named("Shrouded Map")
                .WithShroud(2);

            return builder;
        }

        public static MapBuilder ShroudedHardMazeMapBuilder()
        {
            var builder = MazeMapBuilder()
                .Named("Shrouded Map Hard")
                .WithShroud(1);

            return builder;
        }

        public static MapBuilder RandomHardMazeMapBuilder()
        {
            var generator = new MapGenerator(15, 11);

            var builder = generator.Generate()
                .Named("Random Maze Hard")
                .WithShroud(1);

            return builder;
        }
    }
}
