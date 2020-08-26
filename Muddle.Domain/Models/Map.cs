using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Muddle.Domain.Helpers;

namespace Muddle.Domain.Models
{
    /// <summary>
    /// A container for the game
    /// </summary>
    public class Map
    {
        public Map (int width, int height)
        {
            if (width < Constants.Map.MinWidth)
            {
                throw new Exception($"Map width must be at least {Constants.Map.MinWidth}");
            }

            if (height < Constants.Map.MinHeight)
            {
                throw new Exception($"Map height must be at least {Constants.Map.MinHeight}");
            }

            Width = width;
            Height = height;

            Paths = new List<Path>();
        }

        private int Width { get; set; }

        private int Height { get; set; }

        public int MinX => 0;
        public int MinY => 0;
        public int MaxX => Width - MinX - 1;
        public int MaxY => Height - MinY - 1;

        private List<Path> Paths { get; set; }

        public void AddPath(Path path)
        {
            Paths.Add(path);
        }

        public Point GetPoint(int x, int y)
        {
            if (x < MinX)
            {
                throw new Exception($"Cannot get point where {nameof(x)} is less than {nameof(MinX)} {MinX}");
            }
            if (y < MinY)
            {
                throw new Exception($"Cannot get point where {nameof(y)} is less than {nameof(MinY)} {MinY}");
            }
            if (x > MaxX)
            {
                throw new Exception($"Cannot get point where {nameof(x)} is greater than {nameof(MaxX)} {MaxX}");
            }
            if (y > MaxY)
            {
                throw new Exception($"Cannot get point where {nameof(y)} is greater than {nameof(MaxY)} {MaxY}");
            }

            var point = new Point
            {
                X = x,
                Y = y,
            };

            // find all the path intersections
            foreach (var path in Paths)
            {
                var intersect = point.GetPathIntersect(path);

                if (intersect != null)
                {
                    point.AddPathIntersect(intersect);
                }
            }

            return point;
        }

        public string ToDebugString()
        {
            var sb = new StringBuilder(" ");

            for (var i = 0; i < Width; i++)
            {
                sb.Append(i.ToString().Last());
            }

            sb.Append(Environment.NewLine);

            for (var y = 0; y < Height; y++)
            {
                sb.Append(y.ToString().Last());

                for (var x = 0; x < Width; x++)
                {
                    var point = GetPoint(x, y);

                    sb.Append(point.PathOrientation.CharRepresentation());
                }

                sb.Append(Environment.NewLine);
            }

            return sb.ToString();
        }
    }
}
