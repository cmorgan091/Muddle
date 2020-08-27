using Muddle.Domain.Models;
using System;
using System.Linq;
using FluentAssertions;
using Muddle.Sample.Maps;
using Xunit;

namespace Muddle.Domain.Test
{
    public class JunctionTests
    {
        [Fact]
        public void ForkMap_Point_0_1_has_no_junction()
        {
            // arrange
            var map = new ForkMap().GetMap();
            
            // act
            var point = map.GetPoint(0, 1);

            // assert
            point.HasJunction.Should().BeFalse();
        }
        
        [Fact]
        public void ForkMap_Point_0_2_has_T_junction()
        {
            // arrange
            var map = new ForkMap().GetMap();
            var point = map.GetPoint(0, 2);

            // act
            var junction = point.GetJunction();

            // assert
            junction.Type.Should().Be(Junction.JunctionTypes.TJunction);
        }

        [Fact]
        public void ForkMap_Point_5_2_has_crossroad_junction()
        {
            // arrange
            var map = new ForkMap().GetMap();
            var point = map.GetPoint(5, 2);

            // act
            var junction = point.GetJunction();

            // assert
            junction.Type.Should().Be(Junction.JunctionTypes.Crossroad);
        }

        [Fact]
        public void ForkMap_Point_5_0_has_righthand_junction_south()
        {
            // arrange
            var map = new ForkMap().GetMap();
            var point = map.GetPoint(5, 0);

            // act
            var junction = point.GetJunction();

            // assert
            junction.Type.Should().Be(Junction.JunctionTypes.Righthand);
            junction.FromDirection.Should().Be(Directions.South);
        }

        [Fact]
        public void ForkMap_Point_5_4_has_righthand_junction_east()
        {
            // arrange
            var map = new ForkMap().GetMap();
            var point = map.GetPoint(5, 4);

            // act
            var junction = point.GetJunction();

            // assert
            junction.Type.Should().Be(Junction.JunctionTypes.Righthand);
            junction.FromDirection.Should().Be(Directions.East);
        }
    }
}
