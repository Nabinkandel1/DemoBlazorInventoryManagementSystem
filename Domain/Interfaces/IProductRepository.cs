using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(int id);
        Task<List<Product>> GetAllAsync();
        Task  AddAsync(Product product);
        Task DeleteAsync(int id);
        Task UpdateAsync(Product product);
        Task<List<Product>> GetLowStockAsync(int threshold);
    }
}  
