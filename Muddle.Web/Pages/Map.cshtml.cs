using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Muddle.Domain.Models;
using Muddle.Sample.Maps;

namespace Muddle.Web.Pages
{
    public class MapModel : PageModel
    {
        private readonly ILogger<MapModel> _logger;

        public MapModel(ILogger<MapModel> logger)
        {
            _logger = logger;
            ForkMap = new ForkMap().GetMap();
            CMMap = new CMMap().GetMap();
        }

        public Map ForkMap { get; }
        public Map CMMap { get; }

        public void OnGet()
        {

        }
    }
}
