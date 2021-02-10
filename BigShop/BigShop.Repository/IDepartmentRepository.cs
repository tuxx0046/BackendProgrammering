using BigShop.Models.Department;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BigShop.Repository
{
    public interface IDepartmentRepository
    {
        Task<int> CreateAsync(DepartmentCreate departmentCreate);
        Task<int> DeleteAsync(int departmentId);
        Task<List<Department>> GetAllAsync();
        Task<Department> GetByIdAsync(int departmentId);
        Task<int> UpdateAsync(Department updatedDepartment);
    }
}