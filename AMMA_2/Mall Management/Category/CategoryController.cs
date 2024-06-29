using AMMAAPI.Models;
using AMMAAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace AMMAAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController: ControllerBase
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService) =>
            _categoryService = categoryService;

        [HttpGet]
        public async Task<ActionResult<List<Category>>> Get() =>
            await _categoryService.GetAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> Get(string id)
        {
            var c = await _categoryService.GetByIdAsync(id);

            if (c == null)
            {
                return NotFound();
            }

            return c;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Category c)
        {
            c.CategoryId = ObjectId.GenerateNewId().ToString().Substring(0, 24);

            await _categoryService.CreateAsync(c);

            return CreatedAtAction(nameof(Get), new { id = c.CategoryId }, c);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Category cIn)
        {
            var c = _categoryService.GetByIdAsync;

            if (c == null)
            {
                return NotFound();
            }

            await _categoryService.UpdateAsync(cIn);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var c = await _categoryService.GetByIdAsync(id);

            if (c == null)
            {
                return NotFound();
            }

            await _categoryService.RemoveAsync(c);

            return Ok();
        }
    }
}   

