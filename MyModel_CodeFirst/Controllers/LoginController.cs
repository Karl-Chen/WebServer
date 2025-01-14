using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyModel_CodeFirst.Models;
using NuGet.Protocol;

namespace MyModel_CodeFirst.Controllers
{
    public class LoginController : Controller
    {
        private readonly GuestBookContext _context;
        public LoginController(GuestBookContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login login)
        {
            var result = await _context.Login.Where(m => m.Account == login.Account && m.Password == login.Password).FirstOrDefaultAsync();
            if (result != null)
            {
                //登入完成後須發給證明，證明他已登入
                //使用Session來當全域變數，紀錄登入狀態，須在Program.cs裡面註冊result.ToJson()
                HttpContext.Session.SetString("Manager", result.Account);

                return RedirectToAction("Index", "BooksManage");
            }
            else
            {
                ViewData["Message"] = "帳號或密碼錯誤";
            }
            return View(login);
        }

        public IActionResult Logout()
        {
            //5.4.1 在LoginController加入Logout Action
            HttpContext.Session.Remove("Manager");
            return RedirectToAction("Index", "Home");
        }
    }
    
}
