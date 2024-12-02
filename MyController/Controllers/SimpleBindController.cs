using Microsoft.AspNetCore.Mvc;

namespace MyController.Controllers
{
    public class SimpleBindController : Controller
    {
        // 假設有一個商品資料表，欄位：商品編號、商品名稱、商品價格
        // 1. 先刻一個商品新增的表單介面(把介面實作於View上)
        // 2. 所以要先有一個相對應的action
        // 3. 假設我們的View名稱為Create.cshtml則須有Create() Action
        public IActionResult Create()
        {
            return View();
        }
        // 如果是用Post的話就要加上[HttpPost]才能用post的方式去接收資料
        [HttpPost]
        public IActionResult Create(string id, string name, int price)
        {
            //測試 check
            if (string.IsNullOrEmpty(id))
            {
                ViewData["Result"] = "商品編號必須有值";
                return View();
            }
            if (string.IsNullOrEmpty(name))
            {
                ViewData["Result"] += "商品名稱必須有值";
                return View();
            }
            if (price < 0)
            {
                ViewData["Result"] = "商品價格必須大於等於0";
                return View();
            }
            ViewData["Result"] = $"商品編號：{id}, 商品名稱：{name}, 商品價格：{price}";
            // 接到資料後送資料庫
            //TODO：
            return View();
        }
    }
}
