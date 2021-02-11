using BigShop.DataAccess.Db;
using BigShop.Models.Warehouse;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigShop.Repository
{
    public class WarehouseRepository : IWarehouseRepository
    {
        private readonly IDataAccess _dataAccess;
        private readonly ConnectionStringData _connectionString;

        public WarehouseRepository(IDataAccess dataAccess, ConnectionStringData connectionString)
        {
            _dataAccess = dataAccess;
            _connectionString = connectionString;
        }

        public async Task<int> CreateAsync(WarehouseCreate warehouseCreate)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("Name", warehouseCreate.Name);
            p.Add("AddressLane", warehouseCreate.AddressLane);
            p.Add("Zip_Id", warehouseCreate.Zip_Id);
            p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

            await _dataAccess.SaveData("dbo.spWarehouse_Insert", p, _connectionString.SqlConnectionName);

            return p.Get<int>("Id");
        }

        public Task<int> DeleteAsync(int warehouseId)
        {
            return _dataAccess.SaveData("dbo.spWarehouse_Delete",
                                        new { Id = warehouseId },
                                        _connectionString.SqlConnectionName);
        }

        public Task<List<Warehouse>> GetAllAsync()
        {
            return _dataAccess.LoadData<Warehouse, dynamic>("dbo.spWarehouse_GetAll",
                                                             new { },
                                                             _connectionString.SqlConnectionName);
        }

        public async Task<Warehouse> GetByIdAsync(int warehouseId)
        {
            var recs = await _dataAccess.LoadData<Warehouse, dynamic>("dbo.spWarehouse_GetById",
                                                                       new { Id = warehouseId },
                                                                       _connectionString.SqlConnectionName);
            return recs.FirstOrDefault();
        }

        public Task<int> UpdateAsync(Warehouse updatedWarehouse)
        {
            return _dataAccess.SaveData("dbo.spWarehouse_Update",
                                        new
                                        {
                                            Id = updatedWarehouse.Id,
                                            Name = updatedWarehouse.Name,
                                            AddressLane = updatedWarehouse.AddressLane,
                                            Zip_Id = updatedWarehouse.Zip_Id,
                                        },
                                        _connectionString.SqlConnectionName);
        }
    }
}