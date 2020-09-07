using Muddle.Domain.Generators.DepthFirst;
using Muddle.Domain.Generators.MazesForProgrammers;
using Xunit;

namespace Muddle.Domain.Test
{
    public class GeneratorTests
    {
        [Fact]
        public void MapGenerator_Test()
        {
            var generator = new MapGenerator(5, 5);

            generator.Generate();
        }

        [Fact]
        public void MfpGenerator_Test()
        {
            var generator = new MfpMapGenerator();

            generator.Generate(10, 5);
        }
    }
}
