namespace Muddle.Domain.Models
{
    /// <summary>
    /// A BackgroundItem is an object that we cannot interact with directly, it is there for decoration
    /// </summary>
    public class BackgroundItem
    {
        public int Width { get; set; } = 1;
        public int Height { get; set; } = 1;

        public int TopLeftX { get; set; }
        public int TopLeftY { get; set; }

        public int TileNumber { get; set; } = 1;

        public BackgroundItem(int topLeftX, int topLeftY)
        {
            TopLeftX = topLeftX;
            TopLeftY = topLeftY;
        }
    }
}
