using System.Collections.Generic;
using System.Linq;
using Muddle.Domain.Generators.MazesForProgrammers.Models;

namespace Muddle.Domain.Generators.MazesForProgrammers.MazeMakers
{
    public class WilsonJb
    {
        // This is a C# conversion of the author's code, as opposed to my implementation from his description
        // Whilst my implementation was more consistent in execution time, his was about 30% faster on average
        public static Maze Create(int rows, int cols)
        {
            var maze = new Maze(rows, cols);
            var r = maze.R;
            var unvisited = maze.Cells.ToList();
            var first = unvisited.Rand(r);
            unvisited.Remove(first);

            while (unvisited.Any())
            {
                var next = unvisited.Rand(r);
                var walk = new List<Cell> {next};

                while (unvisited.Contains(next))
                {
                    next = next.Neighbours.Rand(r);
                    if (walk.IndexOf(next) >= 0)
                    {
                        walk = walk.Take(walk.IndexOf(next) + 1).ToList();
                    }
                    else
                    {
                        walk.Add(next);
                    }
                }

                walk.Zip(walk.Skip(1), (thisCell, nextCell) => (thisCell, nextCell))
                    .ForEach(c => c.thisCell.Link(c.nextCell));
                walk.ForEach(c => unvisited.Remove(c));
            }

            return maze;
        }
    }
}