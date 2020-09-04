﻿using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Muddle.Domain.Models;
using Muddle.Web.Models.GameModels;

namespace Muddle.Web.Controllers
{
    [Route("/Game/{gameId}/{action}")]
    public class GameController : Controller
    {
        private readonly IGamesMaster _gamesMaster;

        public GameController(IGamesMaster gamesMaster)
        {
            _gamesMaster = gamesMaster;
        }

        // GET
        public IActionResult Index(Guid gameId)
        {
            var game = _gamesMaster.Games.First(x => x.GameId == gameId);

            var model = new GameIndexModel
            {
                Game = game
            };

            return View(model);
        }
    }
}