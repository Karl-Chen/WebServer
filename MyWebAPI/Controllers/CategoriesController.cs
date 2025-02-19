﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebAPI.DTOs;
using MyWebAPI.Models;
using MyWebAPI.Services;

namespace MyWebAPI.Controllers
{
    //3.1.4 修改API介接路由為「api[controller]」
    [Route("api[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly GoodStoreContext _context;
        private readonly CategoryService _categoryService;

        public CategoriesController(GoodStoreContext context, CategoryService categoryService)
        {
            _context = context;
            _categoryService = categoryService;
        }


        // 直接寫資料庫語法
        //string sql = "Select * from Login where Account = '" + login.Account + "'and Password = '" + login.Password + "'";
        //var result = await _context.Login.FromSqlRaw(sql).FirstOrDefaultAsync();

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategory()
        {
            //4.5.3 改寫CategoriesController裡的第一個Get Action
            //4.5.4 使用Include()同時取得關聯資料並以CategoryDTO傳遞
            //商業邏輯:你要做的功能就叫商業邏輯
            //搬到CategoryServices
            //var category = await _context.Category.Include(c => c.Product).Select(c => GetCategoryDTO(c)).ToListAsync();
            //控制邏輯
            var category = _categoryService.GetCategory();
            if (category == null)
            {
                return NotFound("找不到任何資料");
            }
            return await category;
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetCategory([FromQuery]string id)
        {
            //商業邏輯搬到CategoryServices
            var category = _categoryService.GetCategory(id);
            //var category = await _context.Category.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return await category;
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(string id, [FromForm] CategoryPutOTD category)
        {
            //if (id != category.CateID)
            //{
            //    return BadRequest();
            //}
            if (id == null)
                return BadRequest();

            var cateOld = await _categoryService.FindCategory(id);
            if (cateOld == null)
                return NotFound("查無此資料");

            var cate = _categoryService.UpdateCategory(cateOld, category);
            if (cate == null)
                return NotFound();

            //return NoContent();
            return Ok(cate);
        }

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory([FromForm]CategoryPostOTD category)
        {
            if (_categoryService.CategoryExists(category.CateID))
                return Conflict("資料已存在");
            var cate = await _categoryService.InsertCategory(category);

            if (cate == null)
                return Conflict();

            return CreatedAtAction("GetCategory", new { id = category.CateID }, category);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            var category = await _categoryService.FindCategory(id);
            if (category == null)
            {
                return NotFound();
            }

            _categoryService.DeleteCategory(category);

            return NoContent();
        }

        

    }
}
