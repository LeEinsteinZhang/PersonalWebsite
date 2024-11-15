using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace ZhangLe.Controllers
{
    public class LanguageController : Controller
    {
        public IActionResult ChangeLanguage(string lang)
        {
            if (string.IsNullOrEmpty(lang))
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(lang);
                Thread.CurrentThread.CurrentCulture = new CultureInfo(lang);
            }
            else
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en");
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en");
            }
            Response.Cookies.Append("Language", lang);
            return Redirect(Request.GetTypedHeaders().Referer.ToString());
        }
    }
}
