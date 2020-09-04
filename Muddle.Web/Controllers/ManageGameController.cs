using Microsoft.AspNetCore.Mvc;
using Muddle.Domain.Models;

namespace Muddle.Web.Controllers
{
    [Route("/ManageGame/[action]")]
    public class ManageGameController : Controller
    {
        private readonly IGamesMaster _gamesMaster;

        public ManageGameController(IGamesMaster gamesMaster)
        {
            _gamesMaster = gamesMaster;
        }


        public IActionResult CreateNewGame(int map)
        {
            var mapBuilder = _gamesMaster.MapBuilders[map];
            var game = _gamesMaster.CreateNewGame(mapBuilder);

            return RedirectToAction("Index", "Game", new {gameId = game.GameId});
        }
    }
}