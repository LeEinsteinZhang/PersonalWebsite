using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Resources;
using System.Xml.Linq;

namespace ZhangLe.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("/{expName}")]
        public IActionResult Experiences(string expName)
        {
            int expIndex = 0;
            for (int i = 1; i <= 10; i++)
            {
                var expLink = ZhangLe.Resources.Experiences.ResourceManager.GetString($"Exp{i}Link");
                if (expLink == expName)
                {
                    expIndex = i;
                    break;
                }
            }

            if (expIndex != 0)
            {
                ViewBag.ExpIndex = expIndex;
                return View();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
