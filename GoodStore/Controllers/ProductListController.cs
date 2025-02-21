using GoodStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GoodStore.Controllers
{
    public class ProductListController : Controller
    {
        private readonly GoodStoreContext _context;
        public ProductListController(GoodStoreContext goodStore)
        {
            _context = goodStore;
        }

        public async Task<IActionResult> Index()
        {
            var p = await _context.Product.ToListAsync();
            return View(p);
        }
    }
}
