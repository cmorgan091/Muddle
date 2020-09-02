namespace Muddle.Domain.Models
{
    public class MapBuilder
    {
        private Map _map;

        public MapBuilder(int width, int height)
        {
            _map = new Map(width, height);
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

        public Map Build()
        {
            return _map;
        }
    }
}