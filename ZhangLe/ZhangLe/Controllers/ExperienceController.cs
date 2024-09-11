using Microsoft.AspNetCore.Mvc;
using ZhangLe.ViewModels;
using ZhangLe.ViewModels.Shared;
using System.IO;

namespace ZhangLe.Controllers
{
    public class ExperienceController : BaseController
    {
        [Route("/experience/{id}")]
        public IActionResult Detail(string id)
        {
            var lang = HttpContext.Session.GetString("lang") ?? _defaultLang;
            var experienceModel = LoadExperienceViewModel(lang, id);
            return View(experienceModel);
        }

        private ExperienceViewModel LoadExperienceViewModel(string lang, string id)
        {
            var experienceJsonPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "languages", "experiences", id, $"{lang}.json");

            var experienceModel = LoadJsonFile<ExperienceViewModel>(experienceJsonPath);

            experienceModel.Layout = LoadLayout(lang);

            return experienceModel;
        }
    }
}
