using Microsoft.AspNetCore.Mvc;
using MyView.Models;

namespace MyView.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Login login)
        {
            // 判斷帳號密碼是否正確
            // 導向後台頁面或是前台頁面
            if (login == null || login.Account != "admin" || login.Password != "12345678") {
                ViewData["Err"] = "帳號或密碼有誤";
                return View();
            }
            
            // 導向後台管理頁面
            // 導到別的control action，action先寫第二個input是controller，第三個參數是指定Layout.csthml
            return RedirectToAction("Index", "Home", "_LayoutManger");
        }
    }
}
