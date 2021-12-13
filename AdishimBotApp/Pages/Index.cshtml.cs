using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace AdishimBotApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        public List<string> MSG { get; set; }
        public void OnGet()
        {
            MSG = Logger.Messages;
        }
    }
}
