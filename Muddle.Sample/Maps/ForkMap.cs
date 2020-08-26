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
                    .AddPath(0, 2, 10, Directions.East)
                    .AddPath(5, 0, 5, Directions.South)
                    .AddPath(5, 0, 5, Directions.East)
                    .AddPath(9, 4, 5, Directions.West)
                    .AddPath(0, 3, 3, Directions.North)
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
                    .AddPath(1, 1, 3, Directions.East)
                    .AddPath(1, 1, 5, Directions.South)
                    .AddPath(1, 5, 6, Directions.East)
                    .AddPath(6, 5, 5, Directions.North)
                    .AddPath(6, 1, 3, Directions.East)
                    .AddPath(8, 1, 3, Directions.South)
                    .AddPath(8, 3, 3, Directions.East)
                    .AddPath(10, 3, 3, Directions.North)
                    .AddPath(10, 1, 3, Directions.East)
                    .AddPath(12, 1, 5, Directions.South)
                ;

            return map.Build();
        }
    }
}
