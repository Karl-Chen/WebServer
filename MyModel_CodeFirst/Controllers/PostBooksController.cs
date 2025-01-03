using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyModel_CodeFirst.Models;

namespace MyModel_CodeFirst.Controllers
{
    public class PostBooksController : Controller
    {
        private readonly GuestBookContext _context;

        public PostBooksController(GuestBookContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            //2.1.6 修改Index Action的寫法
            var books = await _context.Book.OrderByDescending(b => b.TimeStmp).ToListAsync();

            return View(books);
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Display(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .FirstOrDefaultAsync(m => m.BookID == id);
            if (book == null)
            {
                return NotFound();
            }
            
            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookID,SN,Title,Description,Author,TimeStmp,PhotoType,Photo")] Book book, IFormFile? newPhoto)
        {
            book.TimeStmp = DateTime.Now;
            string content = book.Description;
            book.Description = content.Replace("/\r/\n", "<br>");
            if (newPhoto != null && newPhoto.Length != 0)
            {
                //執行上傳照片
                //只允許上傳圖檔
                if (newPhoto.ContentType != "image/jpeg" && newPhoto.ContentType != "image/png")
                {
                    ViewData["Message"] = "請上傳正確JPG或PNG格式";
                    return View(book);
                }
                string fileName = book.BookID + ".jpg";
                string BookPhotoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "BookPhotos", fileName);
                using (FileStream stream = new FileStream(BookPhotoPath, FileMode.Create))
                {
                     newPhoto.CopyTo(stream);
                }
                book.PhotoType = newPhoto.ContentType;
                book.Photo = fileName;
            }
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }


        private bool BookExists(string id)
        {
            return _context.Book.Any(e => e.BookID == id);
        }
    }
}
