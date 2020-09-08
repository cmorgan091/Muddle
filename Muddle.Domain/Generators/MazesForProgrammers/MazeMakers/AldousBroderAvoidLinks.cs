using System.Linq;
using Muddle.Domain.Generators.MazesForProgrammers.Models;

namespace Muddle.Domain.Generators.MazesForProgrammers.MazeMakers
{
    public static class AldousBroderAvoidLinks
    {
        // This is a modification of the standard Aldous-Broder algorithm that will avoid visited neighbours (depending on the weighting hard-coded below) if there is a non-visited one available
        public static Maze Create(int rows, int cols)
        {
            var maze = new Maze(rows, cols);
            var r = maze.R;
            var unvisited = rows * cols - 1;
            var current = maze.Cells.Rand(r);
            while (unvisited > 0)
            {
                var next = current.Neighbours.Any(c => r.Next(1000) < 750 && c.Links.Count == 0)
                    ? current.Neighbours.Where(c => c.Links.Count == 0).Rand(r)
                    : current.Neighbours.Rand(r);
                if (next.Links.Count == 0)
                {
                    current.Link(next);
                    unvisited--;
                }

                current = next;
            }

            return maze;
        }
    }
}