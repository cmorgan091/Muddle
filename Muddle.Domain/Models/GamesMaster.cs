using System.Collections.Generic;

namespace Muddle.Domain.Models
{
    public class GamesMaster : IGamesMaster
    {
        public GamesMaster()
        {
            MapBuilders = new List<MapBuilder>();
            Games = new List<Game>();
        }
        
        public List<MapBuilder> MapBuilders { get; set; }

        public List<Game> Games { get; set; }

        public GamesMaster AddMapBuilder(MapBuilder mapBuilder)
        {
            MapBuilders.Add(mapBuilder);

            return this;
        }

        public Game CreateNewGame(MapBuilder mapBuilder)
        {
            var map = mapBuilder.Build();
            var player = new Player();
            player.AddToMap(map, map.GetStartPoint());

            var game = new Game(map, player);

            Games.Add(game);

            return game;
        }
    }
}