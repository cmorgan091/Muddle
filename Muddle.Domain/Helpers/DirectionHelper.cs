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
    }
}