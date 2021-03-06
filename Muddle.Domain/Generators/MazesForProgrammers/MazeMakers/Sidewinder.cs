﻿using System.Collections.Generic;
using Muddle.Domain.Generators.MazesForProgrammers.Models;

namespace Muddle.Domain.Generators.MazesForProgrammers.MazeMakers
{
    public static class Sidewinder
    {
        public static Maze Create(int rows, int cols)
        {
            var maze = new Maze(rows, cols);
            var r = maze.R;
            var run = new List<Cell>();
            maze.Cells.ForEach(c =>
            {
                if (c.WesternBoundary)
                {
                    run = new List<Cell>();
                }

                run.Add(c);
                // If the target is > 50 we favour horizontal runs, if < 50, we favour vertical runs
                if (c.EasternBoundary || !c.NorthernBoundary && r.Next(100) > 50)
                {
                    var cr = run.Rand(r);
                    if (!cr.NorthernBoundary)
                    {
                        cr.Link(cr.North);
                    }

                    run.Clear();
                }
                else
                {
                    c.Link(c.East);
                }
            });
            return maze;
        }
    }
}