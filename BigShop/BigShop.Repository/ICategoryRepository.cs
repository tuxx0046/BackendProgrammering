using BigShop.Models.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigShop.Repository
{
    public interface ICategoryRepository
    {
        public Task<int> CreateAsync(CategoryCreate categoryCreate);
        public Task<List<Category>> GetAllAsync();
        public Task<Category> GetByIdAsync(int categoryId);
        public Task<int> DeleteAsync(int categoryId);
        public Task<int> UpdateAsync(Category updatedCategory);
    }
}
