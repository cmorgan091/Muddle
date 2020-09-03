using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Muddle.Web.Pages
{
    public class InstallationModel : PageModel
    {
        private readonly ILogger<InstallationModel> _logger;

        public InstallationModel(ILogger<InstallationModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
