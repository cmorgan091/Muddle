using System.Collections.Generic;

namespace Muddle.Domain.Models
{
    public interface IGamesMaster
    {
        List<MapBuilder> MapBuilders { get; }
        List<Game> Games { get; }
        GamesMaster AddMapBuilder(MapBuilder mapBuilder);
        Game CreateNewGame(MapBuilder mapBuilder);
    }
}