using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Muddle.Domain.Models;

namespace Muddle.Web.Pages
{
    public class GamesMasterModel : PageModel
    {
        private readonly ILogger<GamesMasterModel> _logger;

        public IGamesMaster GamesMaster { get; set; }

        public GamesMasterModel(ILogger<GamesMasterModel> logger, IGamesMaster gamesMaster)
        {
            _logger = logger;

            GamesMaster = gamesMaster;
        }

        public void OnGet()
        {

        }
    }
}
