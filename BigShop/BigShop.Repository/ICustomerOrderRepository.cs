using BigShop.Models.CustomerOrder;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BigShop.Repository
{
    public interface ICustomerOrderRepository
    {
        Task<int> CreateAsync(CustomerOrderCreate customerOrderCreate);
        Task<int> DeleteAsync(int customerOrderId);
        Task<List<CustomerOrder>> GetAllAsync();
        Task<List<CustomerOrder>> GetAllByCustomerId(int customerId);
        Task<CustomerOrder> GetByIdAsync(int customerOrderId);
    }
}