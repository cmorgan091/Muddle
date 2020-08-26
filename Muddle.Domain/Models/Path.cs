using Muddle.Domain.Helpers;

namespace Muddle.Domain.Models
{
    public class Path
    {
        public Path(int startX, int startY, int length, Directions direction)
        {
            StartX = startX;
            StartY = startY;
            Length = length;
            Direction = direction;
        }

        public int StartX { get; set; }

        public int StartY { get; set; }

        public int Length { get; set; }

        public Directions Direction { get; set; }

        public Orientations Orientation => Direction.Orientation();

    }
}