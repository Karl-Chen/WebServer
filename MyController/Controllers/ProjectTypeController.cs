using Microsoft.AspNetCore.Mvc;
using MyController.Models;

namespace MyController.Controllers
{
    public class ProjectTypeController : Controller
    {
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ProjectType type)
        {
            ViewData["Result"] = $"產品編號：{type.ProjectId}, 產品名稱：{type.Name}, 描述：{type.Description}, 圖片：{type.Picture}";
            return View();
        }
    }
}
