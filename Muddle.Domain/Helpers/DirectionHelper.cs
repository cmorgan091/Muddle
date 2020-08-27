using System;
using Muddle.Domain.Models;

namespace Muddle.Domain.Helpers
{
    public static class DirectionHelper
    {
        public static Orientations Orientation(this Directions direction)
        {
            return (direction == Directions.North || direction == Directions.South)
                ? Orientations.Vertical
                : Orientations.Horizontal;
        }

        public static Directions Opposite(this Directions direction)
        {
            switch (direction)
            {
                case Directions.North:
                    return Directions.South;
                case Directions.East:
                    return Directions.West;
                case Directions.South:
                    return Directions.North;
                case Directions.West:
                    return Directions.East;
            }

            throw new Exception($"Unknown direction {direction}");
        }
    }
}