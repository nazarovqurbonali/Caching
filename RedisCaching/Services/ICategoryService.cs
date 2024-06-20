
namespace RedisCaching.Services;

public interface ICategoryService
{
    Task<Response<List<GetCategoryDto>>> GetCategoriesAsync();
    Task<Response<GetCategoryDto>> GetCategoryByIdAsync(int categoryId);
    Task<Response<string>> CreateCategoryAsync(CreateCategoryDto category);
    Task<Response<string>> UpdateCategoryAsync(UpdateCategoryDto category);
    Task<Response<bool>> DeleteCategoryAsync(int categoryId);
}