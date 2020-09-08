namespace Muddle.Domain.Generators
{
    public class MapBuilderGeneratorModel
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public Algorithms Algorithm = Algorithms.AldousBroderAvoidLinks;
    }
}