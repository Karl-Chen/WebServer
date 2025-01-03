using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyModel_CodeFirst.Models;

namespace MyModel_CodeFirst.Controllers
{
    public class RebooksController : Controller
    {
        private readonly GuestBookContext _context;

        public RebooksController(GuestBookContext context)
        {
            _context = context;
        }

        
        // GET: Rebooks/Create
        public IActionResult Create(string BookID)
        {
            ViewData["BookID"] = BookID;
            return View();
        }

        // POST: Rebooks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReBookID,SN,Description,Author,TimeStmp,BookID")] Rebook rebook)
        {
            rebook.TimeStmp = DateTime.Now;
            if (ModelState.IsValid)
            {
                _context.Add(rebook);
                await _context.SaveChangesAsync();
                //2.5.12 修改RePostBooksController中的Create Action，使其Return JSON資料
                //return RedirectToAction("Display","PostBooks", new { id = rebook.BookID});
                return Json(rebook);
            }
            ViewData["BookID"] = rebook.BookID;
            //return RedirectToAction("Display", "PostBooks", new { id = rebook.BookID });
            return Json(rebook);
        }

        public IActionResult GetRebookByViewComponent(string id)
        {

            return ViewComponent("VCReBooks", new { bookID = id });
        }


    }
}
