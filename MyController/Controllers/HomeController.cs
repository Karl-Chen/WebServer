using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyController.Models;
using static System.Formats.Asn1.AsnWriter;

namespace MyController.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        //-----------------------Action----------------------------
        public IActionResult Index(int score)
        {
            string result = "";
            //呼ノIfStatement?score=87讽盿input狦ㄢinput碞ノ&硈
            if (score >= 90 && score <= 100)
                result = "纔单";
            else if (score >= 80)
                result = "ヒ单";
            else if (score >= 70)
            {
                result = "单";
            }
            else if (score >= 60)
            {
                result = "单";
            }
            else if (score >= 0 && score < 60)
                result = "单";

            ViewData["Level"] = result;

            return View();
        }

        public IActionResult Privacy(int a, int b)
        {
            // ㄢinput碞ノ&硈癬
            ViewData["AaddB"] = a + b;
            return View();
        }
        //-----------------------Action----------------------------
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
