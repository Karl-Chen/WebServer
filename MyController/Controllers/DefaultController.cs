using Microsoft.AspNetCore.Mvc;

namespace MyController.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            ViewData["myTime"] = DateTime.Now;


            return View();
        }

        public IActionResult ShowPhotes()
        {
            //DirectoryInfo di = new DirectoryInfo(@"/images");
            //foreach (FileInfo fi in di.GetFiles())
            //{
            //    ViewData["Photos"] += $"<img src='/images/{fi.Name}' >";
            //}
            for (int i = 1; i <= 8; i++)
                ViewData["Photos"] += $"<a href='/Default/ShowPhoto/{i}'><img src='/images/{i}.jpg' width='200'></a>";
            return View();
        }

        public IActionResult ShowPhoto(int id)
        {
            string[] name = {"櫻桃鴨", "鴨油高麗菜", "鴨油麻婆豆腐", "櫻桃鴨握壽司", "片皮鴨捲三星蔥", "三杯鴨", "慢火白菜鴨", "白鴨煮粥"};
            ViewData["Photo"] = $"<div><img src='/images/{id}.jpg' ></div><h3>{name[id-1]}</h3>";

            return View();
        }
    }
}
