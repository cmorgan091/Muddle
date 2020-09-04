using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Muddle.Domain.Models;
using Muddle.Sample.Maps;

namespace Muddle.Web.Pages
{
    public class MapViewModel : PageModel
    {
        private readonly ILogger<MapViewModel> _logger;

        public Map Map { get; set; }

        public MapViewModel(ILogger<MapViewModel> logger)
        {
            _logger = logger;

            // create a simple map for use with a start and end
            var builder = new MapBuilder(30, 15);

            builder.AddPath(1, 1, Directions.East, 5);
            builder.AddPath(3, 1, Directions.South, 5);
            builder.AddPath(3, 5, Directions.East, 5);

            builder.AddStart(2, 1);
            builder.AddEnd(5, 5);

            Map = builder.Build();

        }

        public void OnGet()
        {

        }
    }
}
