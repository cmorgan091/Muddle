using Muddle.Domain.Models;

namespace Muddle.Domain.Helpers
{
    public static class PathHelper
    {
        public static PathIntersect GetPathIntersect(this Point point, Path path)
        {
            switch (path.Direction)
            {
                case Directions.North:
                    if (point.X == path.StartX && point.Y <= path.StartY && (path.StartY - point.Y) < path.Length)
                    {
                        return new PathIntersect { Path = path, Position = path.StartY - point.Y };
                    }
                    break;

                case Directions.South:
                    if (point.X == path.StartX && point.Y >= path.StartY && (point.Y - path.StartY) < path.Length)
                    {
                        return new PathIntersect { Path = path, Position = point.Y - path.StartY };
                    }
                    break;

                case Directions.West:
                    if (point.Y == path.StartY && point.X <= path.StartX && (path.StartX - point.X) < path.Length)
                    {
                        return new PathIntersect { Path = path, Position = path.StartX - point.X };
                    }
                    break;

                case Directions.East:
                    if (point.Y == path.StartY && point.X >= path.StartX && (point.X - path.StartX) < path.Length)
                    {
                        return new PathIntersect { Path = path, Position = point.X - path.StartX };
                    }
                    break;
            }

            return null;
        }
    }
}