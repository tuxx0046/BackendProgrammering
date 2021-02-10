using BigShop.Models.Employee;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BigShop.Repository
{
    public interface IEmployeeRepository
    {
        Task<int> CreateAsync(EmployeeCreate employeeCreate);
        Task<int> DeleteAsync(int employeeId);
        Task<List<Employee>> GetAllAsync();
        Task<Employee> GetByIdAsync(int employeeId);
        Task<int> UpdateAsync(Employee updatedEmployee);
    }
}