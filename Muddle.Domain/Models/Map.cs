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
            BackgroundItems = new List<BackgroundItem>();
            PointOfInterests = new List<PointOfInterest>();
        }

        public int Width { get; private set; }

        public int Height { get; private set; }

        public int MinX => 0;
        public int MinY => 0;
        public int MaxX => Width - MinX - 1;
        public int MaxY => Height - MinY - 1;

        private List<Path> Paths { get; }
        private List<BackgroundItem> BackgroundItems { get; }
        private List<PointOfInterest> PointOfInterests { get; }

        public void AddPath(Path path)
        {
            Paths.Add(path);
        }

        public void AddPointOfInterest(PointOfInterest pointOfInterest)
        {
            PointOfInterests.Add(pointOfInterest);
        }

        public void AddBackgroundItem(BackgroundItem backgroundItem)
        {
            BackgroundItems.Add(backgroundItem);
        }

        public void AddBackgroundItem(IEnumerable<BackgroundItem> backgroundItems)
        {
            BackgroundItems.AddRange(backgroundItems);
        }

        public PointDetail GetStartPoint()
        {
            var startPoi = PointOfInterests.FirstOrDefault(x => x.Type == PointOfInterestTypes.Start);

            if (startPoi == null)
            {
                throw new Exception($"This map does not include a start point");
            }

            return GetPoint(startPoi.X, startPoi.Y);
        }

        public PointDetail GetPoint(int x, int y)
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

            var point = new PointDetail(x, y);

            // find all the path intersections
            foreach (var path in Paths)
            {
                var intersect = point.GetPathIntersect(path);

                if (intersect != null)
                {
                    point.AddPathIntersect(intersect);
                }
            }

            // find all background intersections
            point.AddBackgroundItem(BackgroundItems.Where(i =>
                i.TopLeftX <= x
                && (i.TopLeftX + i.Width - 1) >= x
                && i.TopLeftY <= y
                && (i.TopLeftY + i.Height - 1) >= y));

            // find all points of interest
            point.AddPointOfInterest(PointOfInterests.Where(i =>
                i.X == x
                && i.Y == y));

            // is this point shrouded?
            point.IsShrouded = ShroudActive && !ShroudRevealedPoints.Contains(point);

            return point;
        }

        public PointDetail GetRelativePoint(PointDetail pointDetail, Directions direction, int positions = 1)
        {
            switch (direction)
            {
                case Directions.North:
                    return GetPoint(pointDetail.X, pointDetail.Y - positions);
                case Directions.South:
                    return GetPoint(pointDetail.X, pointDetail.Y + positions);
                case Directions.West:
                    return GetPoint(pointDetail.X - positions, pointDetail.Y);
                case Directions.East:
                    return GetPoint(pointDetail.X + positions, pointDetail.Y);
            }
            throw new Exception($"Unknown direction {direction}");
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

        /// <summary>
        /// Adding the shroud should be the last thing that is done to a map
        /// </summary>
        /// <param name="revealDistance"></param>
        public void AddShroud(int revealDistance)
        {
            ShroudActive = true;

            ShroudRevealDistance = revealDistance;

            // reveal the area around a start point
            foreach (var startPoint in PointOfInterests.Where(x => x.Type== PointOfInterestTypes.Start))
            {
                RevealShroud(startPoint);
            }
        }

        public void RevealShroud(Point point)
        {
            if (!ShroudActive)
            {
                return;
            }

            // start simple, reveal in all directions
            var minX = GetSafeXValue(point.X - ShroudRevealDistance);
            var minY = GetSafeYValue(point.Y - ShroudRevealDistance);
            var maxX = GetSafeXValue(point.X + ShroudRevealDistance);
            var maxY = GetSafeYValue(point.Y + ShroudRevealDistance);

            for (var x = minX; x <= maxX; x++)
            {
                for (var y = minY; y <= maxY; y++)
                {
                    var p = new Point(x, y);

                    if (!ShroudRevealedPoints.Contains(p))
                    {
                        ShroudRevealedPoints.Add(p);
                    }
                }
            }
        }

        private int GetSafeXValue(int x)
        {
            if (x < MinX)
            {
                x = MinX;
            }

            if (x > MaxX)
            {
                x = MaxX;
            }

            return x;
        }

        private int GetSafeYValue(int y)
        {
            if (y < MinY)
            {
                y = MinY;
            }

            if (y > MaxY)
            {
                y = MaxY;
            }

            return y;
        }

        private bool ShroudActive { get; set; }

        private int ShroudRevealDistance { get; set; }

        /// <summary>
        /// A list of all the points that are no longer covered by the shroud
        /// </summary>
        private List<Point> ShroudRevealedPoints { get; set; } = new List<Point>();



        public void Validate()
        {
            // all points of interest should be on a path
            foreach (var pointOfInterest in PointOfInterests)
            {
                var point = GetPoint(pointOfInterest.X, pointOfInterest.Y);

                if (!point.HasPath)
                {
                    throw new Exception($"{nameof(PointOfInterest)} of type {pointOfInterest.Type} at point {point.X}, {point.Y} does not have a path beneath it");
                }
            }
        }
    }
}
