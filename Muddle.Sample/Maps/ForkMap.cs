using Muddle.Domain.Models;

namespace Muddle.Sample.Maps
{
    public class ForkMap
    {
        public Map GetMap()
        {
            // map should look something like this
            //
            //  0123456789
            // 0     +----
            // 1|    |
            // 2+----+----
            // 3|    |
            // 4     +----

            var map = new MapBuilder(10, 5)
                    .AddPath(0, 2, Directions.East, 10)
                    .AddPath(5, 0, Directions.South, 5)
                    .AddPath(5, 0, Directions.East, 5)
                    .AddPath(9, 4, Directions.West, 5)
                    .AddPath(0, 3, Directions.North, 3)
                ;

            return map.Build();
        }
    }

    public class CMMap
    {
        public Map GetMap()
        {
            // map should look something like this
            //
            //  01234567890123
            // 0 
            // 1 +--  +-+ +-+
            // 2 |    | | | |
            // 3 |    | +-+ |
            // 4 |    |     |
            // 5 +----+     |
            // 6

            var map = new MapBuilder(13, 7)
                    .AddPath(1, 1, Directions.East, 3)
                    .AddPath(1, 1, Directions.South, 5)
                    .AddPath(1, 5, Directions.East, 6)
                    .AddPath(6, 5, Directions.North, 5)
                    .AddPath(6, 1, Directions.East, 3)
                    .AddPath(8, 1, Directions.South, 3)
                    .AddPath(8, 3, Directions.East, 3)
                    .AddPath(10, 3, Directions.North, 3)
                    .AddPath(10, 1, Directions.East, 3)
                    .AddPath(12, 1, Directions.South, 5)
                ;

            return map.Build();
        }
    }
}
