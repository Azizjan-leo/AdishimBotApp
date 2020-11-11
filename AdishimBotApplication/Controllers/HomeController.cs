using System.Web.Mvc;

namespace AdishimBotApplication.Controllers
{
    public class HomeController : Controller
    {
        string answer = "";

        public string Index()
        {
            foreach (var item in Logger.Messages)
            {
                answer += item + "<br/>";
            }
            return answer;
        }
    }
}
