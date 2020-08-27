using System;
using System.Linq;

namespace Muddle.Domain.Models
{
    /// <summary>
    /// When two paths overlap a junction is formed
    /// </summary>
    public class Junction
    {
        private Point _point;

        public JunctionTypes Type { get; }

        public Directions FromDirection { get; }

        public Junction(Point point)
        {
            Guard.NotNull(point, nameof(point));

            _point = point;

            if (_point.PathIntersects.Count != 2)
            {
                throw new Exception($"A junction can only be created on a point which has 2 path intersections, but the point {_point} has {_point.PathIntersects.Count}");
            }

            var details = GetJunctionDetails();

            Type = details.Item1;
            FromDirection = details.Item2;
        }

        private Tuple<JunctionTypes, Directions> GetJunctionDetails()
        {
            // its a valid junction, now we need to work out what type
            var pathIntersectHorizontal = _point.PathIntersects.First(x => x.Path.Orientation == Orientations.Horizontal);
            var pathIntersectVertical = _point.PathIntersects.First(x => x.Path.Orientation == Orientations.Vertical);

            var extendsEast = Convert.ToInt32(pathIntersectHorizontal.Path.Direction == Directions.East
                ? pathIntersectHorizontal.Position < pathIntersectHorizontal.Path.Length - 1
                : pathIntersectHorizontal.Position > 0);

            var extendsWest = Convert.ToInt32(pathIntersectHorizontal.Path.Direction == Directions.West
                ? pathIntersectHorizontal.Position < pathIntersectHorizontal.Path.Length - 1
                : pathIntersectHorizontal.Position > 0);

            var extendsNorth = Convert.ToInt32(pathIntersectVertical.Path.Direction == Directions.North
                ? pathIntersectVertical.Position < pathIntersectVertical.Path.Length - 1
                : pathIntersectVertical.Position > 0);

            var extendsSouth = Convert.ToInt32(pathIntersectVertical.Path.Direction == Directions.South
                ? pathIntersectVertical.Position < pathIntersectVertical.Path.Length - 1
                : pathIntersectVertical.Position > 0);

            // check for a crossroad
            switch (extendsEast + extendsWest + extendsNorth + extendsSouth)
            {
                case 4:
                    return new Tuple<JunctionTypes, Directions>(JunctionTypes.Crossroad, Directions.North);
                case 3:
                    if (extendsNorth == 0)
                        return new Tuple<JunctionTypes, Directions>(JunctionTypes.TJunction, Directions.South);
                    if (extendsSouth == 0)
                        return new Tuple<JunctionTypes, Directions>(JunctionTypes.TJunction, Directions.North);
                    if (extendsEast == 0)
                        return new Tuple<JunctionTypes, Directions>(JunctionTypes.TJunction, Directions.West);
                    
                    return new Tuple<JunctionTypes, Directions>(JunctionTypes.TJunction, Directions.East);
                case 2:
                    if (extendsEast + extendsNorth == 2)
                        return new Tuple<JunctionTypes, Directions>(JunctionTypes.Righthand, Directions.East);
                    if (extendsEast + extendsSouth == 2)
                        return new Tuple<JunctionTypes, Directions>(JunctionTypes.Righthand, Directions.South);
                    if (extendsWest + extendsNorth == 2)
                        return new Tuple<JunctionTypes, Directions>(JunctionTypes.Righthand, Directions.North);
                    if (extendsWest + extendsSouth == 2)
                        return new Tuple<JunctionTypes, Directions>(JunctionTypes.Righthand, Directions.West);
                    break;
            }
            
            throw new Exception($"Unable to calculate junction type and direction");
        }

        public enum JunctionTypes
        {
            Righthand,
            TJunction,
            Crossroad,
        }

        
    }
}