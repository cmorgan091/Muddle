﻿namespace Muddle.Domain.Models
{
    /// <summary>
    /// An intersection of a path and a point
    /// </summary>
    public class PathIntersect
    {
        public Path Path { get; set; }
        public int Position { get; set; }

        public bool IsPathStart => Position == 0;
        public bool IsPathEnd => Position == Path.Length - 1;
    }
}