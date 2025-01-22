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
    public class RebooksController : Controller
    {
        private readonly ExamBookContext _context;

        public RebooksController(ExamBookContext context)
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
        public async Task<IActionResult> Create([Bind("RebookID,Description,Author,TimeStmp,BookID")] Rebook rebook)
        {
            rebook.TimeStmp = DateTime.Now;
            if (ModelState.IsValid)
            {
                _context.Add(rebook);
                await _context.SaveChangesAsync();
                return Json(rebook);
            }
            ViewData["BookID"] = rebook.BookID;
            return Json(rebook);
        }

        // GET: Rebooks/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rebook = await _context.Rebooks.FindAsync(id);
            if (rebook == null)
            {
                return NotFound();
            }
            ViewData["BookID"] = new SelectList(_context.Books, "BookID", "BookID", rebook.BookID);
            return View(rebook);
        }

        public IActionResult GetRebookByViewComponent(string id)
        {

            return ViewComponent("VCReBooks", new { bookID = id });
        }
    }
}
