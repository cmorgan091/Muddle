namespace Muddle.Domain.Generators.MazesForProgrammers.Models
{
    public class CellDistance
    {
        public Cell Cell { get; }
        public int Distance { get; }

        public CellDistance(Cell cell, int distance)
        {
            Cell = cell;
            Distance = distance;
        }
    }
}