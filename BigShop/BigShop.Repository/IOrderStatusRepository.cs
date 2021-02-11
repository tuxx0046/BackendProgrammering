using BigShop.Models.OrderStatus;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BigShop.Repository
{
    public interface IOrderStatusRepository
    {
        Task<int> CreateAsync(OrderStatusCreate orderStatusCreate);
        Task<int> DeleteAsync(int orderStatusId);
        Task<List<OrderStatus>> GetAllAsync();
        Task<OrderStatus> GetByIdAsync(int orderStatusId);
        Task<int> UpdateAsync(OrderStatus updatedOrderStatus);
    }
}