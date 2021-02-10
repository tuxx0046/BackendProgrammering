using BigShop.Models.Customer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BigShop.Repository
{
    public interface ICustomerRepository
    {
        Task<int> CreateAsync(CustomerCreate customerCreate);
        Task<int> DeleteAsync(int customerId);
        Task<List<Customer>> GetAllAsync();
        Task<Customer> GetByApplicationUserIdAsync(int applicationUserId);
        Task<Customer> GetByIdAsync(int customerId);
        Task<int> UpdateAsync(Customer updatedCustomer);
    }
}