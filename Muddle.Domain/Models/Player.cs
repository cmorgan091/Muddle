using System;
using System.Linq;

namespace Muddle.Domain.Models
{
    public class Player
    {
        private Point _currentPoint;

        public DateTime LastMoveDateTime { get; private set; }

        public int X => _currentPoint.X;
        public int Y => _currentPoint.Y;

        public Map Map { get; private set; }

        public bool IsAtStart => _currentPoint.PointOfInterests.Any(x => x.Type == PointOfInterestTypes.Start);
        public bool IsAtEnd => _currentPoint.PointOfInterests.Any(x => x.Type == PointOfInterestTypes.End);

        public bool CanMoveNorth => CanMove(Directions.North);
        public bool CanMoveEast => CanMove(Directions.East);
        public bool CanMoveSouth => CanMove(Directions.South);
        public bool CanMoveWest => CanMove(Directions.West);

        public bool CanMove(Directions direction)
        {
            switch (direction)
            {
                case Directions.North:
                    return _currentPoint.PathExtendsNorth;
                case Directions.East:
                    return _currentPoint.PathExtendsEast;
                case Directions.South:
                    return _currentPoint.PathExtendsSouth;
                case Directions.West:
                    return _currentPoint.PathExtendsWest;
            }

            throw new Exception($"Unknown direction {direction}");
        }

        public void AddToMap(Map map, Point point)
        {
            Map = map;
            _currentPoint = point;
        }

        public void Move(Directions direction)
        {
            if (CanMove(direction))
            {
                var newPoint = Map.GetRelativePoint(_currentPoint, direction);

                _currentPoint = newPoint;
                LastMoveDateTime = DateTime.Now;
            }
            else
            {
                throw new Exception($"Cannot move player in direction {direction}");
            }
        }
    }
}
