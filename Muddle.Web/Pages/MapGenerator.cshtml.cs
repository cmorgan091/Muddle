using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Muddle.Web.Pages
{
    public class MapGeneratorViewModel : PageModel
    {
        private readonly ILogger<MapGeneratorViewModel> _logger;


        public MapGeneratorViewModel(ILogger<MapGeneratorViewModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
