using System;
using System.Diagnostics;
using System.Linq;
using Muddle.Domain.Generators.MazesForProgrammers.MazeMakers;
using Muddle.Domain.Generators.MazesForProgrammers.Models;
using Muddle.Domain.Models;

/*
 * Mazes for Programmers was a book by Jamis Buck which originally gave Maze algorithms in Ruby
 * A port of the code base to C# came from Avrohom Yisroel Silver (Mr Yossu)
 * https://github.com/MrYossu/MazesForProgrammers/
 */


namespace Muddle.Domain.Generators.MazesForProgrammers
{
    public class MfpMapGenerator
    {
        public enum Algorithms
        {
            AldousBroder,
            AldousBroderAvoidLinks,
            AldousBroderWilson,
            BinaryTree,
            Sidewinder,
            Wilson,
            WilsonJb,
        }

        public Maze Maze { get; set; }

        private Maze GetMaze(int width, int height, Algorithms algorithm)
        {
            switch (algorithm)
            {
                case Algorithms.AldousBroderAvoidLinks:
                    return AldousBroderAvoidLinks.Create(height, width);
                case Algorithms.AldousBroder:
                    return AldousBroder.Create(height, width);
                case Algorithms.AldousBroderWilson:
                    return AldousBroderWilson.Create(height, width);
                case Algorithms.BinaryTree:
                    return BinaryTree.Create(height, width);
                case Algorithms.Sidewinder:
                    return Sidewinder.Create(height, width);
                case Algorithms.Wilson:
                    return Wilson.Create(height, width);
                case Algorithms.WilsonJb:
                    return WilsonJb.Create(height, width);
            }

            throw new Exception($"Cannot get maze for algorithm '{algorithm}'");
        }

        public MapBuilder Generate(int width, int height, Algorithms algorithm = Algorithms.AldousBroderAvoidLinks)
        {
            var maze = GetMaze(width, height, algorithm);
            Maze = maze;

            var builder = new MapBuilder(width, height);

            // find all horizontal paths
            for (var y = 0; y < height; y++)
            {
                var onPath = false;
                var pathStart = 0;

                for (var x = 0; x < width; x++)
                {
                    var cell = maze[y, x];
                    if (cell.EasternBoundary || !cell.Links.Contains(cell.East))
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
            for (var x = 0; x < width; x++)
            {
                var onPath = false;
                var pathStart = 0;

                for (var y = 0; y < height; y++)
                {
                    var cell = maze[y, x];
                    if (cell.SouthernBoundary || !cell.Links.Contains(cell.South))
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

            builder.AddStart(maze.LongestPath.First().Col, maze.LongestPath.First().Row);
            builder.AddEnd(maze.LongestPath.Last().Col, maze.LongestPath.Last().Row);

            return builder;
        }

    }
}
