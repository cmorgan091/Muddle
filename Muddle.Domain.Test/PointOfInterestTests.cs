using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Muddle.Domain.Models;
using Xunit;

namespace Muddle.Domain.Test
{
    public class PointOfInterestTests
    {
        private MapBuilder GetBasicMapBuilder()
        {
            var builder = new MapBuilder(30, 15);

            builder.AddPath(1, 1, Directions.East, 5);
            builder.AddPath(3, 1, Directions.South, 5);
            builder.AddPath(3, 5, Directions.East, 5);

            return builder;
        }

        [Fact]
        public void PointOfInterest_on_path_builds()
        {
            // arrange
            var builder = GetBasicMapBuilder();
            builder.AddStart(2, 1);
            builder.AddEnd(5, 5);

            // act
            var map = builder.Build();

            // assert
            map.Should().NotBeNull();
        }

        

        [Fact]
        public void PointOfInterest_off_path_throws_exception()
        {
            // arrange
            var builder = GetBasicMapBuilder();
            builder.AddStart(0, 0);

            // act + assert
            builder.Invoking(x => x.Build()).Should().Throw<Exception>("Start point is not on a path");
        }

    }
}
