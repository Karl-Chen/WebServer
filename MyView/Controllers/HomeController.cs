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
            string[] name = { "���ש]��", "�sԳ���Ӱ�", "���X�]��", "�C�~�]��", "���]��", "�j�F�]��", "�Z�t�]��" };
            string[] address = { "813����������ϸθ۸�", "800�������s���ϥɿŨ�", "800�������s���Ϥ��X�G��",
                "80652�������e��ϳͱۥ|��758��" , "�x�n���_�Ϯ��w���T�q533��", "�x�n���F�ϪL�˸��@�q276��",
                "�x�n������ϪZ�t��69��42��"};
            List<NightMarket> list = new List<NightMarket>();     //ű�y�@�Ӫx������
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

            // LinQ(���Plink)�A�o�N��P select * from list
            var result = from n in list
                         select n;

            //ViewData["id"] = id;

            return View(result);
        }

        public IActionResult IndexRWD()
        {
            List<NightMarket> list = GetNightMarkets();
            // select * from list

            // LinQ(���Plink)�A�o�N��P select * from list
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
