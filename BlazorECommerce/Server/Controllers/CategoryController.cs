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

        [HttpGet("{CategoryId}")]
        public async Task<ActionResult<ServiceResponse<Category>>> GetCategoryById(int CategoryId)
        {
            var result = await _categoryService.GetCategoryByIdAsync(CategoryId);

            if (result != null && result.Data != null)
            {
                return Ok(result);
            }

            return NotFound(result);
        }
    }
}
