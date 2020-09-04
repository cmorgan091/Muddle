using System;
using System.Collections.Generic;
using System.Linq;
using Muddle.Domain.Helpers;

namespace Muddle.Domain.Models
{
    /// <summary>
    /// Defines a single point on a map
    /// </summary>
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public bool HasPath => PathIntersects.Any();

        public override string ToString()
        {
            return $"{X},{Y}";
        }

        public Orientations? PathOrientation
        {
            get
            {
                switch (PathIntersects.Count())
                {
                    case 0:
                        return null;
                    case 1:
                        return PathIntersects.Single().Path.Orientation;
                    case 2:
                        if (PathIntersects.Select(x => x.Path.Orientation).Distinct().Count() == 2)
                        {
                            return Orientations.Both;
                        }

                        break;
                }

                throw new Exception($"Multiple paths in the same orientation were found");
            }
        }

        public bool HasJunction => PathIntersects.Count > 1;

        public Junction GetJunction()
        {
            return new Junction(this);
        }

        public List<PathIntersect> PathIntersects { get; set; } = new List<PathIntersect>();
        public List<BackgroundItem> BackgroundItems { get; set; } = new List<BackgroundItem>();
        public List<PointOfInterest> PointOfInterests { get; set; } = new List<PointOfInterest>();

        public bool PathExtendsNorth => PathIntersects.Any(x =>
            (x.Path.Direction == Directions.North && !x.IsPathEnd) ||
            (x.Path.Direction == Directions.South && !x.IsPathStart));

        public bool PathExtendsSouth => PathIntersects.Any(x =>
            (x.Path.Direction == Directions.South && !x.IsPathEnd) ||
            (x.Path.Direction == Directions.North && !x.IsPathStart));

        public bool PathExtendsEast => PathIntersects.Any(x =>
            (x.Path.Direction == Directions.East && !x.IsPathEnd) ||
            (x.Path.Direction == Directions.West && !x.IsPathStart));

        public bool PathExtendsWest => PathIntersects.Any(x =>
            (x.Path.Direction == Directions.West && !x.IsPathEnd) ||
            (x.Path.Direction == Directions.East && !x.IsPathStart));

        public Directions? PathTerminusDirection
        {
            get
            {
                if (PathIntersects.Count == 1)
                {
                    var pathIntersect = PathIntersects.Single();
                    if (pathIntersect.IsPathStart)
                    {
                        return pathIntersect.Path.Direction.Opposite();
                    }

                    if (pathIntersect.IsPathEnd)
                    {
                        return pathIntersect.Path.Direction;
                    }
                }

                return null;
            }
        }

        public void AddPathIntersect(PathIntersect pathIntersect)
        {
            PathIntersects.Add(pathIntersect);
        }

        public void AddPointOfInterest(PointOfInterest pointOfInterest)
        {
            PointOfInterests.Add(pointOfInterest);
        }

        public void AddPointOfInterest(IEnumerable<PointOfInterest> pointOfInterests)
        {
            PointOfInterests.AddRange(pointOfInterests);
        }

        public void AddBackgroundItem(BackgroundItem backgroundItem)
        {
            BackgroundItems.Add(backgroundItem);
        }

        public void AddBackgroundItem(IEnumerable<BackgroundItem> backgroundItem)
        {
            BackgroundItems.AddRange(backgroundItem);
        }
    }
}