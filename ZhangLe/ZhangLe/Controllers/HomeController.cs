using Microsoft.AspNetCore.Mvc;
using ZhangLe.ViewModels;
using ZhangLe.ViewModels.Shared;
using System.IO;

namespace ZhangLe.Controllers
{
    public class HomeController : BaseController
    {
        [Route("/")]
        public IActionResult Index()
        {
            var lang = HttpContext.Session.GetString("lang") ?? _defaultLang;
            var homeViewModel = LoadHomeViewModel(lang);
            return View(homeViewModel);
        }

        private HomeViewModel LoadHomeViewModel(string lang)
        {
            var homeJsonPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "languages", "home", $"{lang}.json");

            var homeViewModel = LoadJsonFile<HomeViewModel>(homeJsonPath);

            homeViewModel.Layout = LoadLayout(lang);

            return homeViewModel;
        }
    }
}
