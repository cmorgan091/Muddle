﻿using System.Collections.Generic;
using System.Linq;
using Muddle.Domain.Generators.MazesForProgrammers.Models;

namespace Muddle.Domain.Generators.MazesForProgrammers.MazeMakers
{
    public static class BinaryTree
    {
        public static Maze Create(int rows, int cols)
        {
            var maze = new Maze(rows, cols);
            var r = maze.R;
            maze.Cells.ForEach(c =>
            {
                var neighbours = new List<Cell> {c.North, c.East}.Where(c1 => c1 != null).ToList();
                if (neighbours.Count == 1)
                {
                    c.Link(neighbours.Single());
                }
                else if (neighbours.Count == 2)
                {
                    // The line below biases the split between vertical and horizontal corridors. If the bias is > 50, we get longer horizontal corridors, if < 50, we get longer vertical corridors
                    c.Link(neighbours.Skip(r.Next(100) > 50 ? 0 : 1).First());
                }
            });
            return maze;
        }
    }
}