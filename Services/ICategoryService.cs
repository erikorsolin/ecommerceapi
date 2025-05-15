using EcommerceApi.DTOs;

namespace EcommerceApi.Services
{
    public interface ICategoryService
    {
        Task<List<CategoryDTO>> GetAllCategoriesAsync();
        Task<CategoryDTO?> GetCategoryByIdAsync(int id);
        Task<CategoryDTO> CreateCategoryAsync(CreateCategoryDTO dto);
        Task<bool> UpdateCategoryAsync(int id, CreateCategoryDTO dto);
        Task<bool> DeleteCategoryAsync(int id);
    }
}