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
            //���}�᭱��IfStatement?score=87�۷��ainput�A�p�G���input�N��&�۳s
            if (score >= 90 && score <= 100)
                result = "�u��";
            else if (score >= 80)
                result = "�ҵ�";
            else if (score >= 70)
            {
                result = "�A��";
            }
            else if (score >= 60)
            {
                result = "����";
            }
            else if (score >= 0 && score < 60)
                result = "�B��";

            ViewData["Level"] = result;

            return View();
        }

        public IActionResult Privacy(int a, int b)
        {
            // ���input�N��&�۳s�b�@�_
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
