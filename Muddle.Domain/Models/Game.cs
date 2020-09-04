using System;

namespace Muddle.Domain.Models
{
    public class Game
    {
        public Game(Map map, Player player)
        {
            Guard.AllNotNull(map, player);

            GameId = Guid.NewGuid();
            Map = map;
            Player = player;
            StartedDateTime = DateTime.Now;

            Player.AddToMap(Map, Map.GetStartPoint());
        }

        public Guid GameId { get; }

        public Map Map { get; }

        public Player Player { get; }

        public DateTime StartedDateTime { get; }

        public DateTime LastUpdatedDateTime => Player.LastMoveDateTime;
    }
}
