using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Muddle.Domain.Models;
using Muddle.Sample.Maps;

namespace Muddle.Web.Pages
{
    public class MapModel : PageModel
    {
        private readonly ILogger<MapModel> _logger;
        private readonly Map _map;

        public MapModel(ILogger<MapModel> logger)
        {
            _logger = logger;
            _map = new CMMap().GetMap();
        }

        public Map Map => _map;

        public void OnGet()
        {

        }
    }
}
