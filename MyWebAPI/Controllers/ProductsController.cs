using System;
using System.Collections.Generic;
using System.Drawing.Design;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyWebAPI.DTOs;
using MyWebAPI.Models;

namespace MyWebAPI.Controllers
{
    [Route("api[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly GoodStoreContextCustom _context;

        //4.7.8 修改ProductsController上方所注入的MyStoreContext為MyStoreContext2
        public ProductsController(GoodStoreContextCustom context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProduct(string? cateID, string? id, string? ProductName, decimal? minPrice, decimal? maxPrice, string? descript, decimal price = 0)
        {
            //decimal較精準的double/float
            //4.1.2 使用Include()同時取得關聯資料 inner join => select Product.* from Product inner join Cate on ....所以還是只有Product的內容
            //var products = await _context.Product.Include(c=>c.Cate).ToListAsync();
            //4.1.3 使用Where()改變查詢的條件並測試
            //var products = await _context.Product.Include(c => c.Cate).Where(p=>p.Price >= price).ToListAsync();
            //4.1.4 使用OrderBy()相關排序方法改變資料排序並測試
            //var products = await _context.Product.Include(c => c.Cate).OrderBy(p => p.Price).ToListAsync();
            //4.1.5 使用Select()抓取特定的欄位並測試，用select重新選取要讀取的欄位
            //var products = await _context.Product.Include(c => c.Cate).OrderBy(p => p.Price).
            //    Select(p => new Product 
            //    {
            //        ProductID = p.ProductID,
            //        ProductName = p.ProductName,
            //        Price = p.Price,
            //        Description = p.Description,
            //        Picture = p.Picture,
            //        CateID = p.CateID,
            //        Cate = p.Cate
            //    }) .ToListAsync();
            //4.2.3 改寫ProductsController裡的GetAction

            //var products = await _context.Product.Include(c => c.Cate).OrderBy(p => p.Price).
            //    Select(p => ItemProduct(p)).ToListAsync();
            //4.4.8 修改先將資料載入內存的寫法(改用AsQueryable，搜尋放到資料庫去做)
            var products = _context.Product.Include(c => c.Cate).OrderBy(p => p.Price).AsQueryable();
            //4.4.1 加入產品類別搜尋
            if (!string.IsNullOrEmpty(cateID))
            {
                products = products.Where(p => p.CateID == cateID);
            }
            //4.4.2 加入產品名稱關鍵字搜尋
            if (!string.IsNullOrEmpty(ProductName))
            {
                products = products.Where(p => p.ProductName.Contains(ProductName));
            }
            //4.4.3 加入價格區間搜區
            if (minPrice.HasValue && maxPrice.HasValue)
            {
                products = products.Where(p => p.Price > minPrice && p.Price < maxPrice);
            }
            //4.4.4 加入產品敘述關鍵字搜尋
            if (!descript.IsNullOrEmpty())
            {
                products = products.Where(p => p.Description.Contains(descript));
            }
            
            if (products == null || products.Count() == 0)
                return NotFound("找不到產品資料");
            //var product = await _context.Product.Include(c => c.Cate).Where(p => p.CateID == cateID).Where(p=>p.ProductName.Contains(ProductName))
            //    Select(p => ItemProduct(p)).TOListAsync();
            return await products.Select(p=>ItemProduct(p)).ToArrayAsync();
        }

        [HttpGet("fromSQL")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductFromSQL(string? cateID, string? id, string? ProductName, decimal? minPrice, decimal? maxPrice, string? descript, decimal price = 0)
        {
            string sql = "Select p.ProductID, p.ProductName, p.Price, p.Description, p.CateID, c.CateName from Category as c" +
                " inner join [Product] as p on c.CateID=p.CateID where 1=1";

            List<SqlParameter> parameters = new List<SqlParameter>();
            
            //4.6.3 製作關鍵字查詢
            if (!string.IsNullOrEmpty(cateID))
            {
                //sql += " and p.CateID='" + cateID + "'";
                sql += " and p.CateID=@cateID";
                parameters.Add(new SqlParameter("@cateID", cateID));
            }
            //4.4.2 加入產品名稱關鍵字搜尋
            if (!string.IsNullOrEmpty(ProductName))
            {
                //sql += " and p.ProductName like %'" + ProductName + "%'";
                sql += " and p.ProductName like @ProductName";
                parameters.Add(new SqlParameter("@ProductName", $"%{ProductName}%"));
            }
            if (minPrice.HasValue && maxPrice.HasValue)
            {
                //sql += " and p.Price between " + minPrice + " and " + maxPrice;
                sql += " and p.Price between @minPrice and @maxPrice";
                parameters.Add(new SqlParameter("@minPrice", minPrice));
                parameters.Add(new SqlParameter("@maxPrice", maxPrice));
            }
            //4.4.4 加入產品敘述關鍵字搜尋
            if (!descript.IsNullOrEmpty())
            {
                sql += " and p.Description like @Descript";
                parameters.Add(new SqlParameter("@Descript", $"%{descript}%"));
            }

            var ret = await _context.ProductDTO.FromSqlRaw(sql, parameters.ToArray()).ToListAsync();

            if (ret == null )
            {
                return NotFound("找不到產品資料");
            }
            return ret;
        }

        [HttpGet("fromProc/{id}")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductFromProc(string id)
        {
            //4.8.2 在ProductsController中建立一個新的Get Action
            //4.8.3 設置介接口為[HttpGet("fromProc/{id}")]，Action名稱可自訂，並使用ProductDTO來傳遞資料
            //4.8.4 使用預存程序進行查詢(參數的傳遞請使用SqlParameter)
            //4.8.5 使用Swagger測試
            string sql = "exec getProductWithCateName @cateID";
            var CateID = new SqlParameter("@cateID", id);
            //執行SQL
            var products = await _context.ProductDTO.FromSqlRaw(sql, CateID).ToListAsync();
            if (products == null)
                return NotFound("找不到商品資料");
            return products;
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(string id)
        {
            //原本的
            //var product = await _context.Product.FindAsync(id);

            //4.3   取得特定資料(ProductsController裡的第二個Get Action)
            //var product = await _context.Product.Include(c => c.Cate).Where(p => p.ProductID == id).
            //    Select(p => ItemProduct(p)).FirstOrDefaultAsync();

            //4.4   建立Product資料查詢功能
            //4.4.1 加入產品類別搜尋
            var product = await _context.Product.Include(c => c.Cate).Where(p => p.ProductID == id).
                Select(p => ItemProduct(p)).FirstOrDefaultAsync();
            
            if (product == null)
            {
                return NotFound("找不到產品資料");
            }

            return product;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(string id, Product product)
        {
            if (id != product.ProductID)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.Product.Add(product);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProductExists(product.ProductID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProduct", new { id = product.ProductID }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Product.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(string id)
        {
            return _context.Product.Any(e => e.ProductID == id);
        }

        private static ProductDTO ItemProduct(Product p)
        {
            var result = new ProductDTO
            {
                ProductID = p.ProductID,
                ProductName = p.ProductName,
                Price = p.Price,
                Description = p.Description,
                CateID = p.Cate.CateID,
                CateName = p.Cate.CateName,
                Picture = p.Picture
            };
            return result;
        }
    }
}
