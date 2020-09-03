using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Muddle.Domain.Models;
using Muddle.Sample.Maps;

namespace Muddle.Web.Pages
{
    public class MapViewModel : PageModel
    {
        private readonly ILogger<MapViewModel> _logger;

        public MapViewModel(ILogger<MapViewModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
