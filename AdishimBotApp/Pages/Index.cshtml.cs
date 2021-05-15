using System.Collections.Generic;
using System.Net;
using System.IO;    // For StreamReader
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
            try {
                var request = (HttpWebRequest)WebRequest.Create("http://azizjan-001-site3.etempurl.com/Alerter");
                var response = (HttpWebResponse)request.GetResponse();
            } catch (System.Exception e) {

            }
           

            //var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            MSG = Logger.Messages;
        }
    }
}
