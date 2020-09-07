using System;
using System.Collections.Generic;

namespace Muddle.Domain.Models
{
    public class MapBuilder
    {
        private int? _shroudRevealDistance;

        public string Name { get; private set; }
        public int Width { get; }
        public int Height { get; }

        private List<Action<Map>> _mapActions;
        
        public MapBuilder(int width, int height)
        {
            Width = width;
            Height = height;

            _mapActions = new List<Action<Map>>();
        }

        private MapBuilder AddAction(Action<Map> action)
        {
            _mapActions.Add(action);

            return this;
        }

        public MapBuilder Named(string name)
        {
            Name = name;

            return this;
        }

        public MapBuilder AddPath(int startX, int startY, Directions direction, int length)
        {
            var path = new Path(startX, startY, direction, length);

            return AddAction(map => map.AddPath(path));
        }

        public MapBuilder AddBackgroundItem(BackgroundItem backgroundItem)
        {
            return AddAction(map => map.AddBackgroundItem(backgroundItem));
        }

        public MapBuilder AddBackgroundItem(int topLeftX, int topLeftY)
        {
            return AddBackgroundItem(new BackgroundItem(topLeftX, topLeftY));
        }

        private MapBuilder AddPointOfInterest(int x, int y, PointOfInterestTypes type)
        {
            return AddAction(map => map.AddPointOfInterest(new PointOfInterest(x, y, type)));
        }

        public MapBuilder AddStart(int x, int y)
        {
            return AddPointOfInterest(x, y, PointOfInterestTypes.Start);
        }

        public MapBuilder AddEnd(int x, int y)
        {
            return AddPointOfInterest(x, y, PointOfInterestTypes.End);
        }

        public MapBuilder WithShroud(int revealDistance = 2)
        {
            _shroudRevealDistance = revealDistance;
            
            return this;
        }



        public Map Build()
        {
            if (_shroudRevealDistance.HasValue)
            {
                _mapActions.Add(x => x.AddShroud(_shroudRevealDistance.Value));
            }

            // create the map
            var map = new Map(Width, Height);

            // apply actions
            _mapActions.ForEach(x => x.Invoke(map));

            map.Validate();

            return map;
        }
    }
}