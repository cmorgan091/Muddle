namespace Muddle.Domain.Models
{
    public class MapBuilder
    {
        private Map _map;
        private int? _shroudRevealDistance;

        public string Name { get; private set; }
        public int Width => _map.Width;
        public int Height => _map.Height;
        

        public MapBuilder(int width, int height)
        {
            _map = new Map(width, height);
        }

        public MapBuilder Named(string name)
        {
            Name = name;

            return this;
        }

        public MapBuilder AddPath(int startX, int startY, Directions direction, int length)
        {
            var path = new Path(startX, startY, direction, length);

            _map.AddPath(path);

            return this;
        }

        public MapBuilder AddBackgroundItem(BackgroundItem backgroundItem)
        {
            _map.AddBackgroundItem(backgroundItem);

            return this;
        }

        public MapBuilder AddBackgroundItem(int topLeftX, int topLeftY)
        {
            return AddBackgroundItem(new BackgroundItem(topLeftX, topLeftY));
        }

        private MapBuilder AddPointOfInterest(int x, int y, PointOfInterestTypes type)
        {
            _map.AddPointOfInterest(new PointOfInterest(x, y, type));

            return this;
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
                _map.AddShroud(_shroudRevealDistance.Value);
            }

            _map.Validate();

            return _map;
        }
    }
}