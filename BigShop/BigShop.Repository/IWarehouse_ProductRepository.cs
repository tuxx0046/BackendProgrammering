using BigShop.Models.Warehouse_Product;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BigShop.Repository
{
    public interface IWarehouse_ProductRepository
    {
        Task<int> CreateAsync(Warehouse_ProductCreate warehouseProductCreate);
        Task<int> DeleteByProductIdAsync(int productId);
        Task<int> DeleteByWarehouseIdAsync(int warehouseId);
        Task<List<Warehouse_Product>> GetAllAsync();
        Task<List<Warehouse_Product>> GetByProductIdAsync(int productId);
        Task<List<Warehouse_Product>> GetByWarehouseIdAsync(int warehouseId);
        Task<int> UpdateQuantityAsync(Warehouse_Product warehouse_Product);
        Task<Warehouse_Product> GetByProductIdAndWarehouseIdAsync(int productId, int warehouseId);
    }
}