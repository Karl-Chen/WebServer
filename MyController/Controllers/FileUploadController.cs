using System.IO;
using Microsoft.AspNetCore.Mvc;


//上傳檔案用的方式
namespace MyController.Controllers
{
    public class FileUploadController : Controller
    {
        public IActionResult Create()
        {
            return View();
        }
        //IFormFile上傳檔案用的格式
        [HttpPost]
        public IActionResult Create(IFormFile photo)
        {
            if (photo == null || photo.Length == 0)
            {
                ViewData["Message"] = "沒有上傳任何檔案或檔案已損毀!!";
                return View();
            }
            // 只能上傳圖片
            if (photo.ContentType != "image/jpeg" && photo.ContentType != "image/png")
            {
                ViewData["Message"] = "請上傳圖片jpg或png檔!!";
                return View();
            }

            //取得檔名稱
            string fileName = Path.GetFileName(photo.FileName);
            // 用一個filePath變數儲存路徑
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "photos", fileName);
            // 把檔案儲存於伺服器上
            // using相當於try catch，而且using跟FileStream共用，如果FileStream出錯，會自動關閉FileStream通道
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                photo.CopyTo(stream);
            }

            ViewData["Message"] = "上傳完成!!";

            return View();
        }
    }
}
