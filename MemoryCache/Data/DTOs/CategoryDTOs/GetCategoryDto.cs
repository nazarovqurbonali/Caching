namespace MemoryCache.Data.DTOs.CategoryDTOs;

public class GetCategoryDto
{
    public int Id { get; set; }
    public required string? Name { get; set; }
    public string? Description { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}