using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace AdishimBotApp.Pages
{
    public class AlerterModel : PageModel
    {
        public async Task OnGet()
        {
            Task.Run(() => Kek());
        }

        async Task Kek() {
            Thread.Sleep(800000);
            var request2 = (HttpWebRequest)WebRequest.Create("http://azizjan-001-site3.etempurl.com/Alerter");
            var response = (HttpWebResponse)request2.GetResponse();
        }
    }
}
