using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context; //For DbContext Manuplation

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            var categoryList = await _context.Categories.ToListAsync();
            if (categoryList == null || !categoryList.Any())
            {
                return new List<Category>();
            }
            return categoryList;
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            var result = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == id);
            if (result == null)
            {
                return null; // Return null if no category found
            }
            return result;
        }

        public async Task AddAsync(Category category)
        {
            var categoryAdd = _context.Categories.AddAsync(category);
            if (categoryAdd == null)
            {
                throw new ArgumentNullException(nameof(category), "Category cannot be null");
            }
            var categoryList = categoryAdd.ToString(); // Return the added category
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            var categoryUpdate = _context.Categories.Update(category);
            if (categoryUpdate == null)
            {
                throw new ArgumentNullException(nameof(category), "Category cannot be null");
            }
            var categoryList = categoryUpdate.ToString(); // Return the updated category
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }
    }
}
