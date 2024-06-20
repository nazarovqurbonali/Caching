using RedisCaching.Data;
using RedisCaching.Data.DTOs.CategoryDTOs;
using RedisCaching.Data.Entities;
using RedisCaching.Data.Responses;

namespace RedisCaching.Services;

public class CategoryService(DataContext context) : ICategoryService
{
    public async Task<Response<List<GetCategoryDto>>> GetCategoriesAsync()
    {
        try
        {
            var categories = await context.Categories.Select(x => new GetCategoryDto()
            {
                Name = x.Name,
                Id = x.Id,
                Description = x.Description,
                CreatedAt = x.CreatedAt
            }).ToListAsync();

            return new Response<List<GetCategoryDto>>(categories);
        }
        catch (Exception e)
        {
            return new Response<List<GetCategoryDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetCategoryDto>> GetCategoryByIdAsync(int categoryId)
    {
        try
        {
            var existingCategory = await context.Categories.Where(x => x.Id == categoryId).Select(x =>
                new GetCategoryDto()
                {
                    Name = x.Name,
                    Id = x.Id,
                    Description = x.Description,
                    CreatedAt = x.CreatedAt
                }).FirstOrDefaultAsync();
            if (existingCategory == null)
            {
                return new Response<GetCategoryDto>(HttpStatusCode.BadRequest, "Category not found");
            }

            return new Response<GetCategoryDto>(existingCategory);
        }
        catch (Exception e)
        {
            return new Response<GetCategoryDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<string>> CreateCategoryAsync(CreateCategoryDto? category)
    {
        try
        {
            if (category is null) return new Response<string>(HttpStatusCode.BadRequest, "Category cannot be null");
            var existingCategory = await context.Categories.AnyAsync(x => x.Name == category.Name);
            if (existingCategory)
            {
                return new Response<string>(HttpStatusCode.BadRequest, "Category already exists");
            }

            var newCategory = new Category
            {
                Name = category.Name,
                Description = category.Description,
                CreatedAt = DateTimeOffset.UtcNow,
            };
            await context.Categories.AddAsync(newCategory);
            await context.SaveChangesAsync();

            return new Response<string>("Successfully created category");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<string>> UpdateCategoryAsync(UpdateCategoryDto category)
    {
        try
        {
            var existingCategory = await context.Categories.FirstOrDefaultAsync(x => x.Id == category.Id);
            
                
            if (existingCategory == null)
            {
                return new Response<string>(HttpStatusCode.BadRequest, $"Not found category with Id={category.Id}");
            }
            
            existingCategory.Description=category.Description;
            existingCategory.Name=category.Name;
            await context.SaveChangesAsync();

            return new Response<string>("Successfully updated category");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteCategoryAsync(int categoryId)
    {
        try
        {
            var existingCategory = await context.Categories.FirstOrDefaultAsync(x => x.Id == categoryId);

            if (existingCategory == null)
            {
                return new Response<bool>(HttpStatusCode.BadRequest, "Category not found");
            }

            context.Categories.Remove(existingCategory);
            await context.SaveChangesAsync();
            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}