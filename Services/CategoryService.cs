using EcommerceApi.Data;
using EcommerceApi.DTOs;
using EcommerceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.Services
{

    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CategoryDTO>> GetAllCategoriesAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            return categories.Select(c => new CategoryDTO
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();
        }

        public async Task<CategoryDTO?> GetCategoryByIdAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return null;

            return new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public async Task<CategoryDTO> CreateCategoryAsync(CreateCategoryDTO dto)
        {
            var category = new Category
            {
                Name = dto.Name
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public async Task<bool> UpdateCategoryAsync(int id, CreateCategoryDTO dto)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return false;

            category.Name = dto.Name;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return false;

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}