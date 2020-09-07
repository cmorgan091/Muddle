namespace Muddle.Domain.Models
{
    public class PointOfInterest : Point
    {
        public PointOfInterest(int x, int y, PointOfInterestTypes type) : base(x, y)
        {
            Type = type;
        }

        public PointOfInterestTypes Type { get; set; }
    }
}