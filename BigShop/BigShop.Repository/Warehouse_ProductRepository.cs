using BigShop.DataAccess.Db;
using BigShop.Models.Warehouse_Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigShop.Repository
{
    public class Warehouse_ProductRepository : IWarehouse_ProductRepository
    {
        private readonly IDataAccess _dataAccess;
        private readonly ConnectionStringData _connectionString;

        public Warehouse_ProductRepository(IDataAccess dataAccess, ConnectionStringData connectionString)
        {
            _dataAccess = dataAccess;
            _connectionString = connectionString;
        }

        public Task<int> CreateAsync(Warehouse_ProductCreate warehouseProductCreate)
        {
            return _dataAccess.SaveData("dbo.spWarehouse_Product_Insert",
                                        warehouseProductCreate,
                                        _connectionString.SqlConnectionName);
        }

        public Task<int> DeleteByProductIdAsync(int productId)
        {
            return _dataAccess.SaveData("dbo.spWarehouse_Product_DeleteByProductId",
                                        new { Product_Id = productId },
                                        _connectionString.SqlConnectionName);
        }

        public Task<int> DeleteByWarehouseIdAsync(int warehouseId)
        {
            return _dataAccess.SaveData("dbo.spWarehouse_Product_DeleteByWarehouseId",
                                        new { Warehouse_Id = warehouseId },
                                        _connectionString.SqlConnectionName);
        }

        public Task<List<Warehouse_Product>> GetAllAsync()
        {
            return _dataAccess.LoadData<Warehouse_Product, dynamic>("dbo.spWarehouse_Product_GetAll",
                                                                    new { },
                                                                    _connectionString.SqlConnectionName);
        }

        public Task<List<Warehouse_Product>> GetByProductIdAsync(int productId)
        {
            return _dataAccess.LoadData<Warehouse_Product, dynamic>("dbo.spWarehouse_Product_GetByProductId",
                                                                    new { Product_Id = productId },
                                                                    _connectionString.SqlConnectionName);
        }

        public Task<List<Warehouse_Product>> GetByWarehouseIdAsync(int warehouseId)
        {
            return _dataAccess.LoadData<Warehouse_Product, dynamic>("dbo.spWarehouse_Product_GetByWarehouseId",
                                                                    new { Warehouse_Id = warehouseId },
                                                                    _connectionString.SqlConnectionName);
        }

        public async Task<Warehouse_Product> GetByProductIdAndWarehouseIdAsync(int productId, int warehouseId)
        {
            var recs = await _dataAccess.LoadData<Warehouse_Product, dynamic>("dbo.spWarehouse_Product_GetByProductIdAndWarehouseId",
                                                                    new 
                                                                    { 
                                                                        Product_Id = productId, 
                                                                        Warehouse_Id = warehouseId
                                                                    },
                                                                    _connectionString.SqlConnectionName);
            return recs.FirstOrDefault();
        }

        public Task<int> UpdateQuantityAsync(Warehouse_Product warehouse_Product)
        {
            return _dataAccess.SaveData("dbo.spWarehouse_Product_UpdateQuantity",
                                        new
                                        {
                                            Quantity = warehouse_Product.Quantity,
                                            Product_Id = warehouse_Product.Product_Id,
                                            Warehouse_Id = warehouse_Product.Warehouse_Id
                                        },
                                        _connectionString.SqlConnectionName);
        }
    }
}
