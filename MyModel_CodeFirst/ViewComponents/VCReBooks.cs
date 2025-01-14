using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyModel_CodeFirst.Models;

namespace MyModel_CodeFirst.ViewComponents
{
    //2.3.3 VCReBooks class繼承ViewComponent(注意using Microsoft.AspNetCore.Mvc;)
    //2.3.4 撰寫InvokeAsync()方法取得回覆留言資料
    public class VCReBooks : ViewComponent
    {
        private readonly GuestBookContext _context;
        public VCReBooks(GuestBookContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(string bookID, bool isDel=false)
        {
            // select * from ReBook
            // where BookID = @BookID
            // order by TimpStamp desc
            var db = await _context.Rebook.Where(r => r.BookID == bookID).OrderByDescending(r => r.TimeStmp).ToListAsync();
            if (isDel)
                return View("Delete", db);

            return View(db);
        }
    }
}
