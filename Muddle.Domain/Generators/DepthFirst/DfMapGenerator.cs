using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Muddle.Domain.Models;
using Point = System.Drawing.Point;

namespace Muddle.Domain.Generators.DepthFirst
{
    /// <summary>
    /// A MapGenerator can be used to automate the creation of a MapBuilder and will create a random map each time
    /// It uses a Randomized depth-first search algorithm: https://en.wikipedia.org/wiki/Maze_generation_algorithm#Randomized_depth-first_search
    /// </summary>
    public class DfMapGenerator
    {
        private readonly CellState[,] _cells;
        private readonly int _width;
        private readonly int _height;
        private readonly Random _random;

        private Point _start;
        private Point _end;
        private int _maxDistance;
 
        public DfMapGenerator(int width, int height)
        {
            _width = width;
            _height = height;
            _cells = new CellState[width, height];
            for(var x=0; x<width; x++)
            {
                for(var y=0; y < height; y++)
                {
                    _cells[x, y] = CellState.Initial;
                }
            }

            _random = new Random();

            _start = new Point(_random.Next(width), _random.Next(height));
            VisitCell(_start.X, _start.Y, 0);
        }

        public struct RemoveWallAction
        {
            public Point Neighbour;
            public CellState Wall;
        }

        [Flags]
        public enum CellState
        {
            Top = 1,
            Right = 2,
            Bottom = 4,
            Left = 8,
            Visited = 128,
            Initial = Top | Right | Bottom | Left,
        }
 
        public CellState this[int x, int y]
        {
            get => _cells[x, y];
            set => _cells[x, y] = value;
        }
 
        public IEnumerable<RemoveWallAction> GetNeighbours(Point p)
        {
            if (p.X > 0)
            {
                yield return new RemoveWallAction {Neighbour = new Point(p.X - 1, p.Y), Wall = CellState.Left};
            }

            if (p.Y > 0)
            {
                yield return new RemoveWallAction {Neighbour = new Point(p.X, p.Y - 1), Wall = CellState.Top};
            }

            if (p.X < _width-1)
            {
                yield return new RemoveWallAction {Neighbour = new Point(p.X + 1, p.Y), Wall = CellState.Right};
            }

            if (p.Y < _height-1)
            {
                yield return new RemoveWallAction {Neighbour = new Point(p.X, p.Y + 1), Wall = CellState.Bottom};
            }
        }
 
        public void VisitCell(int x, int y, int distance)
        {
            this[x, y] |= CellState.Visited;

            if (distance > _maxDistance)
            {
                _end = new Point(x, y);
                _maxDistance = distance;
            }

            foreach (var p in GetNeighbours(new Point(x, y))
                .Shuffle(_random)
                .Where(z => !(this[z.Neighbour.X, z.Neighbour.Y]
                    .HasFlag(CellState.Visited))))
            {
                this[x, y] -= p.Wall;
                this[p.Neighbour.X, p.Neighbour.Y] -= p.Wall.OppositeWall();
                VisitCell(p.Neighbour.X, p.Neighbour.Y, distance + 1);
            }
        }
 
        public MapBuilder Generate()
        {
            var builder = new MapBuilder(_width, _height);

            // find all horizontal paths
            for (var y = 0; y < _height; y++)
            {
                var onPath = false;
                var pathStart = 0;

                for (var x = 0; x < _width; x++)
                {
                    if (this[x, y].HasFlag(CellState.Right))
                    {
                        // so no path to the right
                        if (onPath)
                        {
                            // we've reached the end
                            onPath = false;
                            Debug.WriteLine($"Easterly path from [{pathStart},{y}] to [{x},{y}]");
                            builder.AddPath(pathStart, y, Directions.East, x - pathStart + 1);
                        }
                    }
                    else
                    {
                        if (!onPath)
                        {
                            onPath = true;
                            pathStart = x;
                        }
                    }
                }
            }

            // find all vertical paths
            for (var x = 0; x < _width; x++)
            {
                var onPath = false;
                var pathStart = 0;

                for (var y = 0; y < _height; y++)
                {
                    if (this[x, y].HasFlag(CellState.Bottom))
                    {
                        // so no path to the bottom
                        if (onPath)
                        {
                            // we've reached the end
                            onPath = false;
                            Debug.WriteLine($"Southerly path from [{x},{pathStart}] to [{x},{y}]");
                            builder.AddPath(x, pathStart, Directions.South, y - pathStart + 1);
                        }
                    }
                    else
                    {
                        if (!onPath)
                        {
                            onPath = true;
                            pathStart = y;
                        }
                    }
                }
            }

            builder.AddStart(_start.X, _start.Y);
            builder.AddEnd(_end.X, _end.Y);

            return builder;
        }
    }
}
