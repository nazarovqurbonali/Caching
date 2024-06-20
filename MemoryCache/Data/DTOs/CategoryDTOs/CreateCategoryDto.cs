namespace MemoryCache.Data.DTOs.CategoryDTOs;

public class CreateCategoryDto
{
    public required string Name { get; set; } 
    public string? Description { get; set; }
}