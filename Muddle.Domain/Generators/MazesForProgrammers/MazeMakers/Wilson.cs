﻿using System.Collections.Generic;
using System.Linq;
using Muddle.Domain.Generators.MazesForProgrammers.Models;

namespace Muddle.Domain.Generators.MazesForProgrammers.MazeMakers
{
    public static class Wilson
    {
        public static Maze Create(int rows, int cols)
        {
            var maze = new Maze(rows, cols);
            var r = maze.R;
            var walk = new List<Cell>();
            // Pick a random cell to mark as visited. This will be the end point of our first walk
            var first = maze.Cells.Rand(r);
            // Pick a starting cell for our first walk
            var current = maze.Cells.Where(c => c.Row != first.Row && c.Col != first.Col).Rand(r);
            walk.Add(maze.Cells.First(c => c.Row == current.Row && c.Col == current.Col));
            while (maze.Cells.Any(c => !c.Links.Any()))
            {
                var next = walk.Last().Neighbours.Rand(r);
                if (next == first || next.Links.Any())
                {
                    walk.Add(next);
                    // Carve cells along the current walk
                    walk.Zip(walk.Skip(1), (thisCell, nextCell) => (thisCell, nextCell))
                        .ForEach(c => c.thisCell.Link(c.nextCell));
                    // If there are any unvisited cells, start a new walk from a random one
                    if (maze.Cells.Any(c => !c.Links.Any()))
                    {
                        walk = new List<Cell> {maze.Cells.Where(c => !c.Links.Any()).Rand(r)};
                    }
                }
                else if (walk.Contains(next))
                {
                    // Snip off all cells after the last time we visited this cell
                    walk = walk.Take(walk.IndexOf(next) + 1).ToList();
                }
                else
                {
                    walk.Add(next);
                }
            }

            return maze;
        }
    }
}