using System.Collections.Generic;
using Muddle.Domain.Models;

namespace Muddle.Domain.Generators
{
    public class MapBuilderGeneratorModel
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public Algorithms Algorithm { get; set; }= Algorithms.AldousBroderAvoidLinks;
        public List<Point> BlockedPoints { get; set; }
    }
}