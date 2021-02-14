using BigShop.Models.OrderLine;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BigShop.Repository
{
    public interface IOrderLineRepository
    {
        Task<int> CreateAsync(List<OrderLineCreate> orderLineCreate);
        Task<int> DeleteByCustomerOrderIdAsync(int customerOrderId);
        Task<List<OrderLine>> GetByCustomerOrderIdAsync(int customerOrderId);
        Task<List<OrderLine>> GetByProductIdAsync(int productId);
    }
}