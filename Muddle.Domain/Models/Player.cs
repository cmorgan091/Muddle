using System;
using System.Linq;

namespace Muddle.Domain.Models
{
    public class Player
    {
        private PointDetail _currentPointDetail;

        public DateTime LastMoveDateTime { get; private set; }

        public int X => _currentPointDetail.X;
        public int Y => _currentPointDetail.Y;

        public Map Map { get; private set; }

        public bool IsAtStart => _currentPointDetail.PointOfInterests.Any(x => x.Type == PointOfInterestTypes.Start);
        public bool IsAtEnd => _currentPointDetail.PointOfInterests.Any(x => x.Type == PointOfInterestTypes.End);

        public bool CanMoveNorth => CanMove(Directions.North);
        public bool CanMoveEast => CanMove(Directions.East);
        public bool CanMoveSouth => CanMove(Directions.South);
        public bool CanMoveWest => CanMove(Directions.West);

        public int MovesMade { get; private set; }

        public bool CanMove(Directions direction)
        {
            switch (direction)
            {
                case Directions.North:
                    return _currentPointDetail.PathExtendsNorth;
                case Directions.East:
                    return _currentPointDetail.PathExtendsEast;
                case Directions.South:
                    return _currentPointDetail.PathExtendsSouth;
                case Directions.West:
                    return _currentPointDetail.PathExtendsWest;
            }

            throw new Exception($"Unknown direction {direction}");
        }

        public void AddToMap(Map map, PointDetail pointDetail)
        {
            Map = map;
            _currentPointDetail = pointDetail;
        }

        public Point Move(Directions direction)
        {
            if (CanMove(direction))
            {
                var newPoint = Map.GetRelativePoint(_currentPointDetail, direction);

                _currentPointDetail = newPoint;
                LastMoveDateTime = DateTime.Now;
                MovesMade++;

                return _currentPointDetail;
            }
            else
            {
                throw new Exception($"Cannot move player in direction {direction}");
            }
        }
    }
}
