using CodeFirstModelExam.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeFirstModelExam.ViewComponents
{
    public class VCReBooks:ViewComponent
    {
        private readonly ExamBookContext _context;
        public VCReBooks(ExamBookContext context) { _context = context; }

        public async Task<IViewComponentResult> InvokeAsync(string bookID)
        {
            var db = await _context.Rebooks.Where(t => t.BookID == bookID).OrderByDescending(t => t.TimeStmp).ToArrayAsync();
            return View(db);
        }
    }
}
