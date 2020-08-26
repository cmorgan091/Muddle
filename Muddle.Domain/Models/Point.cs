using System;
using System.Collections.Generic;
using System.Linq;

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

        public List<PathIntersect> PathIntersects { get; set; } = new List<PathIntersect>();

        public void AddPathIntersect(PathIntersect pathIntersect)
        {
            PathIntersects.Add(pathIntersect);
        }
    }
}