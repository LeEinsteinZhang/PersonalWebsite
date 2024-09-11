using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ZhangLe.ViewModels.Shared;
using System.IO;

namespace ZhangLe.Controllers
{
    public class BaseController : Controller
    {
        protected readonly string _defaultLang = "en";

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
