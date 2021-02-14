using BigShop.Models.OrderProcess;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BigShop.Repository
{
    public interface IOrderProcessRepository
    {
        Task<int> CreateAsync(OrderProcessCreate orderProcessCreate);
        Task<int> DeleteByCustomerOrderIdAsync(int customerOrderId);
        Task<List<OrderProcess>> GetAllAsync();
        Task<List<OrderProcess>> GetByCustomerOrderIdAsync(int customerOrderId);
        Task<List<OrderProcess>> GetByEmployeeIdAsync(int employeeId);
        Task<List<OrderProcess>> GetByOrderStatusIdAsync(int orderStatusId);
    }
}