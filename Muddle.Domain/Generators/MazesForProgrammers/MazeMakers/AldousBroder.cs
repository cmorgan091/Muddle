using Muddle.Domain.Generators.MazesForProgrammers.Models;

namespace Muddle.Domain.Generators.MazesForProgrammers.MazeMakers
{
    public static class AldousBroder
    {
        public static Maze Create(int rows, int cols)
        {
            var maze = new Maze(rows, cols);
            var r = maze.R;
            var unvisited = rows * cols - 1;
            var current = maze.Cells.Rand(r);
            while (unvisited > 0)
            {
                var next = current.Neighbours.Rand(r);
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