using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyView.Models;

namespace MyView.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        private List<NightMarket> GetNightMarkets()
        {
            string[] id = { "A01", "A02", "A03", "A04", "A05", "A06", "A07" };
            string[] name = { "瑞豐夜市", "新堀江商圈", "六合夜市", "青年夜市", "花園夜市", "大東夜市", "武聖夜市" };
            string[] address = { "813高雄市左營區裕誠路", "800高雄市新興區玉衡里", "800高雄市新興區六合二路",
                "80652高雄市前鎮區凱旋四路758號" , "台南市北區海安路三段533號", "台南市東區林森路一段276號",
                "台南市中西區武聖路69巷42號"};
            List<NightMarket> list = new List<NightMarket>();     //鑄造一個泛型物件
            for (int i = 0; i < id.Length; i++)
            {
                NightMarket nightMarket = new NightMarket();
                nightMarket.ID = id[i];
                nightMarket.Name = name[i];
                nightMarket.Address = address[i];
                list.Add(nightMarket);
            }
            return list;
        }

        public IActionResult Index()
        {
            List<NightMarket> list = GetNightMarkets();
            // select * from list

            // LinQ(音同link)，這意思同 select * from list
            var result = from n in list
                         select n;

            //ViewData["id"] = id;

            return View(result);
        }

        public IActionResult IndexRWD()
        {
            List<NightMarket> list = GetNightMarkets();
            // select * from list

            // LinQ(音同link)，這意思同 select * from list
            var result = from n in list
                         orderby n.ID descending
                         select n;

            //ViewData["id"] = id;

            return View(result);
        }

        public IActionResult Razor()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
