using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorECommerce.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<Category>>>> GetAllCategories()
        {
            var result = await _categoryService.GetAllCategoriesAsync();
            return Ok(result);
        }

        [HttpGet("admin"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<List<Category>>>> GetAdminCategoriesAsync()
        {
            var result = await _categoryService.GetAdminCategoriesAsync();
            return Ok(result);
        }

        [HttpPost("admin"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<List<Category>>>> AddCategoryAsync(Category Category)
        {
            var result = await _categoryService.AddCategoryAsync(Category);
            return Ok(result);
        }

        [HttpPut("admin"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<List<Category>>>> UpdateCategoryAsync(Category Category)
        {
            var result = await _categoryService.UpdateCategoryAsync(Category);
            return Ok(result);
        }

        [HttpDelete("admin/{CategoryId}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<List<Category>>>> DeleteCategoryAsync(int CategoryId)
        {
            var result = await _categoryService.DeleteCategoryAsync(CategoryId);
            return Ok(result);
        }
    }
}
