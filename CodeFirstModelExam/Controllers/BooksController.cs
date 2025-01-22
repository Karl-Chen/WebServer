using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CodeFirstModelExam.Models;

namespace CodeFirstModelExam.Controllers
{
    public class BooksController : Controller
    {
        private readonly ExamBookContext _context;

        public BooksController(ExamBookContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var book = await _context.Books.OrderByDescending(t => t.TimeStmp).ToArrayAsync();
            return View(book);
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Content(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
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
        public async Task<IActionResult> Create([Bind("BookID,Title,Description,Author,TimeStmp,PhotoType,Photo")] Book book, IFormFile? newPhoto)
        {
            book.TimeStmp = DateTime.Now;
            string descript = book.Description;
            book.Description = descript.Replace("\r\n", "<br>");
            if (newPhoto != null && newPhoto.Length > 0)
            {
                if (newPhoto.ContentType != "image/jpeg" && newPhoto.ContentType != "image/png")
                {
                    ViewData["ErrMsg"] = "請上傳正確JPG或PNG格式";
                    return View(book);
                }
                string subFileName = newPhoto.ContentType.Substring(6, newPhoto.ContentType.Length - 6);
                string fileName = book.BookID + "." + subFileName;
                //string fileName = book.BookID + ".jpg";
                string BookPhotoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "BookPhotos", fileName);
                using (FileStream stream = new FileStream(BookPhotoPath, FileMode.Create))
                {
                    newPhoto.CopyTo(stream);
                }
                book.Photo = fileName;
                book.PhotoType = newPhoto.ContentType;

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
            return _context.Books.Any(e => e.BookID == id);
        }
    }
}
