using Microsoft.AspNetCore.Mvc;
using ZhangLe.ViewModels;
using ZhangLe.ViewModels.Shared;
using System.IO;

namespace ZhangLe.Controllers
{
    public class ExperiencesController : BaseController
    {
        [Route("/{experience}")]
        public IActionResult Experience(string experience)
        {   
            var lang = HttpContext.Session.GetString("Lang") ?? "en";
            var currentPage = HttpContext.Session.GetString("CurrentPage") ?? experience;
            var viewModel = LoadExperienceViewModel(lang, experience);

            return View(viewModel);
        }

        private ExperienceViewModel LoadExperienceViewModel(string lang, string experience)
        {
            var experienceJsonPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "languages", "experiences", $"{experience}", $"{lang}.json");
            var viewModel = LoadJsonFile<ExperienceViewModel>(experienceJsonPath);
            viewModel.Layout = LoadLayout(lang);
            return viewModel;
        }
    }
}
