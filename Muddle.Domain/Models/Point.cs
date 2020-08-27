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
    }
}