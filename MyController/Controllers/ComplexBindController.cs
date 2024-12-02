using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using MyController.Models;

namespace MyController.Controllers
{
    // 假設有一個會員資料表，欄位：會員編號、會員姓名、地址、電話、性別
    // 1. 撰寫資料模型(Model)
    // 2. 先刻一個加入會員的表單介面(把介面實作於View上)
    // 3. 所以要先有一個相對應的action
    // 4. 假設我們的View名稱為Create.cshtml則須有Create() Action
    //[HttpPost]很重要，一定要記得加!!

    public class ComplexBindController : Controller
    {
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Member member)
        {
            ViewData["Result"] = $"會員編號：{member.MemberID}, 會員姓名：{member.MemberName}, 地址：{member.MemberAddres}, 電話：{member.MemberPhone}, 姓別：{member.Gender}";
            return View();
        }
    }
}
