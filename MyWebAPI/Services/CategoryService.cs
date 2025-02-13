using Microsoft.EntityFrameworkCore;
using MyWebAPI.DTOs;
using MyWebAPI.Models;

namespace MyWebAPI.Services
{
    //8.2.1 在Service資料夾中建立CategoryService，並將CategoriesController裡的兩個Get Action相關的商業邏輯移至此撰寫
    //      (包括ItemProduct()方法一併移入CategoryService)
    //8.2.2 複製一個CategoriesController，並把檔名及class名字改掉(這個動作做不做都可以，只是要保留舊的寫法供參考用)

    //商業邏輯(取物給物)都放在Services裡面，控制邏輯(if)放controller

    public class CategoryService
    {
        private readonly GoodStoreContext _context;

        public CategoryService(GoodStoreContext context)
        {
            _context = context;
        }

        //從資料庫取得產品類別的清單
        /// <summary>
        /// 從資料庫取得所有產品類別的清單
        /// </summary>
        /// <returns></returns>
        public async Task<List<CategoryDTO>> GetCategory()
        {
            var category = _context.Category.Include(c => c.Product).Select(c => GetCategoryDTO(c));
            return await category.ToListAsync();
        }

        //以多載實作GetCategory
        /// <summary>
        /// 從資料庫取得指定產品類別的資料(傳入PK)，回傳單一資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<CategoryDTO> GetCategory(string id)
        {
            var category = await _context.Category.Include(c => c.Product).Where(c => c.CateID == id)
                .Select(c => GetCategoryDTO(c)).FirstOrDefaultAsync();
            return category;
        }

        public async Task<Category> FindCategory(string id)
        {
            var catid = _context.Category.FindAsync(id);
            return await catid;
        }

        public async Task<Category> UpdateCategory(Category cate, CategoryPutOTD category)
        {
            cate.CateName = category.CateName;

            _context.Entry(cate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return cate;
        }

        public async Task<Category> InsertCategory(CategoryPostOTD category)
        {
            Category cate = new Category();
            cate.CateName = category.CateName;
            cate.CateID = category.CateID;
            _context.Category.Add(cate);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw;
            }
            return cate;
        }

        public async void DeleteCategory(Category category)
        {
            _context.Category.Remove(category);
            await _context.SaveChangesAsync();
            return;
        }

        private static CategoryDTO GetCategoryDTO(Category c)
        {
            var categoryDTO = new CategoryDTO
            {
                CateID = c.CateID,
                CateName = c.CateName,
                Product = c.Product

            };
            return categoryDTO;
        }

        public bool CategoryExists(string id)
        {
            return _context.Category.Any(e => e.CateID == id);
        }
    }
}
