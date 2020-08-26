using Muddle.Domain.Models;
using System;
using System.Linq;
using FluentAssertions;
using Muddle.Sample.Maps;
using Xunit;

namespace Muddle.Domain.Test
{
    public class MapTests
    {
        [Fact]
        public void Map_must_be_wider_than_minimum()
        {
            // arrange
            var testWidth = Constants.Map.MinWidth - 1;

            // act
            Func<Map> action = () => new Map(testWidth, 100);

            // assert
            action.Should().Throw<Exception>();
        }

        [Fact]
        public void Map_must_be_higher_than_minimum()
        {
            // arrange
            var testHeight = Constants.Map.MinHeight - 1;

            // act
            Func<Map> action = () => new Map(100, testHeight);

            // assert
            action.Should().Throw<Exception>();
        }

        private Map GetForkMap()
        {
            // map should look something like this
            //
            //  0123456789
            // 0     +----
            // 1|    |
            // 2+----+----
            // 3|    |
            // 4     +----

            return new ForkMap().GetMap();
        }

        [Fact]
        private void ToDebugString()
        {
            var map = GetForkMap();

            var str = map.ToDebugString();
        }

        [Fact]
        public void ForkMap_Point_0_0_has_no_path()
        {
            // arrange
            var map = GetForkMap();

            // act
            var point = map.GetPoint(0, 0);

            // assert
            point.HasPath.Should().BeFalse();
        }

        [Fact]
        public void ForkMap_Point_0_1_has_vertical_path()
        {
            // arrange
            var map = GetForkMap();

            // act
            var point = map.GetPoint(0, 1);

            // assert
            point.HasPath.Should().BeTrue();
            point.PathIntersects.Count.Should().Be(1);
            point.PathOrientation.Should().Be(Orientations.Vertical);
        }

        [Fact]
        public void ForkMap_Point_0_2_has_multiple_paths()
        {
            // arrange
            var map = GetForkMap();

            // act
            var point = map.GetPoint(0, 2);

            // assert
            point.HasPath.Should().BeTrue();
            point.PathIntersects.Count.Should().Be(2);
            point.PathOrientation.Should().Be(Orientations.Both);
        }
    }
}
