using Microsoft.AspNetCore.Mvc;
using quizon.Models;
using quizon.Services;

namespace quizon.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        ICategoryService categoryService;
        public CategoryController(ICategoryService service)
        {
            categoryService = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(categoryService.Get());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetId(int id)
        {
            var category = await categoryService.GetId(id);
            return Ok(category);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Category category)
        {
            var newCategory = await categoryService.Save(category);
            return Ok(newCategory);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Category category)
        {
            var updated = await categoryService.Update(id, category);
            return Ok(updated);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await categoryService.Delete(id);
            return Ok();
        }
    }
}