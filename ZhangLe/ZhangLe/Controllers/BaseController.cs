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
            // 检查并设置Session中的语言信息
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Lang")))
            {
                HttpContext.Session.SetString("Lang", "en"); // 设置默认语言为 "en"
            }

            // 检查并设置Session中的前一个页面信息
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("PrevUrl")))
            {
                HttpContext.Session.SetString("PrevUrl", Request.Path); // 设置当前请求路径作为前一个页面信息
            }
        }

        protected T LoadJsonFile<T>(string filePath) where T : new()
        {
            if (!System.IO.File.Exists(filePath))
            {
                return new T();
            }

            var jsonData = System.IO.File.ReadAllText(filePath);

            // 如果反序列化失败，将返回一个新的对象而不是 null
            return JsonConvert.DeserializeObject<T>(jsonData) ?? new T();
        }

        protected LayoutViewModel LoadLayout(string lang)
        {
            var layoutJsonPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "languages", "layout", $"{lang}.json");

            return LoadJsonFile<LayoutViewModel>(layoutJsonPath);
        }
    }
}
