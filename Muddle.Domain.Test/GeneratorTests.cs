using Muddle.Domain.Generator;
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
    }
}
