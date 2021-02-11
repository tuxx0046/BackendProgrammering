using BigShop.Models.Product;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BigShop.Repository
{
    public interface IProductRepository
    {
        Task<int> CreateAsync(ProductCreate productCreate);
        Task<int> DeleteAsync(int productId);
        Task<List<Product>> GetAllAsync();
        Task<List<Product>> GetByCategoryIdAsync(int categoryId);
        Task<Product> GetByIdAsync(int productId);
        Task<List<Product>> GetByManufacturerIdAsync(int manufacturerId);
        Task<int> UpdateAsync(Product updatedProduct);
    }
}