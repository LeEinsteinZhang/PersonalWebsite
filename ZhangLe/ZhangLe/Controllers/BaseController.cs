using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ZhangLe.ViewModels.Shared;
using System.IO;

namespace ZhangLe.Controllers
{
    public class BaseController : Controller
    {

        protected void EnsureSession()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Lang")))
            {
                HttpContext.Session.SetString("Lang", "en");
            }

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("PrevUrl")))
            {
                HttpContext.Session.SetString("PrevUrl", Request.Path);
            }
        }

        protected T LoadJsonFile<T>(string filePath) where T : new()
        {
            if (!System.IO.File.Exists(filePath))
            {
                return new T();
            }

            var jsonData = System.IO.File.ReadAllText(filePath);

            return JsonConvert.DeserializeObject<T>(jsonData) ?? new T();
        }

        protected LayoutViewModel LoadLayout(string lang)
        {
            var layoutJsonPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "languages", "layout", $"{lang}.json");

            return LoadJsonFile<LayoutViewModel>(layoutJsonPath);
        }

        public IActionResult SwitchLang(string lang, string next)
        {
            if (string.IsNullOrEmpty(lang) || (lang != "en" && lang != "zh"))
            {
                lang = "en";
            }

            HttpContext.Session.SetString("Lang", lang);

            return Redirect(next ?? "/");
        }
    }
}
