using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace ZhangLe.Controllers
{
    public class LanguageController : Controller
    {
        private static readonly HashSet<string> SupportedLanguages = new()
        {
            "en",
            "zh"
        };

        public IActionResult ChangeLanguage(string lang)
        {
            if (!string.IsNullOrEmpty(lang) && SupportedLanguages.Contains(lang))
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(lang);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);
                Response.Cookies.Append("Language", lang);
            }
            else
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
            }
            return Redirect(Request.Headers["Referer"].ToString() ?? "/");
        }

    }
}
