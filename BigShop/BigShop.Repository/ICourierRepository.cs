using BigShop.Models.Courier;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BigShop.Repository
{
    public interface ICourierRepository
    {
        Task<int> CreateAsync(CourierCreate courierCreate);
        Task<int> DeleteAsync(int courierId);
        Task<List<Courier>> GetAllAsync();
        Task<Courier> GetByIdAsync(int courierId);
        Task<int> UpdateAsync(Courier updatedCourier);
    }
}