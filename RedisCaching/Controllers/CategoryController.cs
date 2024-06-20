using Microsoft.AspNetCore.Mvc;
using RedisCaching.Data.DTOs.CategoryDTOs;
using RedisCaching.Services;

namespace RedisCaching.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CategoryController(ICategoryService categoryService):ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetCategoriesAsync()
    {
        var response = await categoryService.GetCategoriesAsync();
        return StatusCode(response.StatusCode, response);
    }
    
    [HttpGet("{categoryId:int}")]
    public async Task<IActionResult> GetCategoryByIdAsync(int categoryId)
    {
        var response = await categoryService.GetCategoryByIdAsync(categoryId);
        return StatusCode(response.StatusCode, response);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateCategoryAsync(CreateCategoryDto create )
    {
        var response = await categoryService.CreateCategoryAsync(create);
        return StatusCode(response.StatusCode, response);
    }
    [HttpPut]
    public async Task<IActionResult> UpdateCategoryAsync(UpdateCategoryDto update)
    {
        var response = await categoryService.UpdateCategoryAsync(update);
        return StatusCode(response.StatusCode, response);
    }
    
    [HttpDelete("{categoryId:int}")]
    public async Task<IActionResult> DeleteCategoryAsync(int categoryId)
    {
        var response = await categoryService.DeleteCategoryAsync(categoryId);
        return StatusCode(response.StatusCode, response);
    }
    
}