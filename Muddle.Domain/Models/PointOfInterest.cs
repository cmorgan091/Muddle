namespace Muddle.Domain.Models
{
    public class PointOfInterest
    {
        public PointOfInterest(int x, int y, PointOfInterestTypes type)
        {
            X = x;
            Y = y;
            Type = type;
        }

        public PointOfInterestTypes Type { get; set; }

        public int X { get; set; }

        public int Y { get; set; }
    }
}