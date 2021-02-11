using BigShop.Models.Warehouse;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BigShop.Repository
{
    public interface IWarehouseRepository
    {
        Task<int> CreateAsync(WarehouseCreate warehouseCreate);
        Task<int> DeleteAsync(int warehouseId);
        Task<List<Warehouse>> GetAllAsync();
        Task<Warehouse> GetByIdAsync(int warehouseId);
        Task<int> UpdateAsync(Warehouse updatedWarehouse);
    }
}