using BigShop.DataAccess.Db;
using BigShop.Models.OrderProcess;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigShop.Repository
{
    public class OrderProcessRepository
    {
        private readonly IDataAccess _dataAccess;
        private readonly ConnectionStringData _connectionString;

        public OrderProcessRepository(IDataAccess dataAccess, ConnectionStringData connectionString)
        {
            _dataAccess = dataAccess;
            _connectionString = connectionString;
        }

        public Task<int> CreateAsync(OrderProcessCreate orderProcessCreate)
        {
            return _dataAccess.SaveData("dbo.spOrderProcess_Insert",
                                        new 
                                        { 
                                            Employee_Id = orderProcessCreate.Employee_Id,
                                            CustomerOrder_Id = orderProcessCreate.CustomerOrder_Id,
                                            OrderStatus_Id = orderProcessCreate.OrderStatus_Id
                                        },
                                        _connectionString.SqlConnectionName);
        }

        public Task<int> DeleteAsync(int manufacturerId)
        {
            return _dataAccess.SaveData("dbo.spManufacturer_Delete",
                                        new { Id = manufacturerId },
                                        _connectionString.SqlConnectionName);
        }

        public Task<List<Manufacturer>> GetAllAsync()
        {
            return _dataAccess.LoadData<Manufacturer, dynamic>("dbo.spManufacturer_GetAll",
                                                             new { },
                                                             _connectionString.SqlConnectionName);
        }

        public async Task<Manufacturer> GetByIdAsync(int manufacturerId)
        {
            var recs = await _dataAccess.LoadData<Manufacturer, dynamic>("dbo.spManufacturer_GetById",
                                                                       new { Id = manufacturerId },
                                                                       _connectionString.SqlConnectionName);
            return recs.FirstOrDefault();
        }

        public Task<int> UpdateAsync(Manufacturer updatedManufacturer)
        {
            return _dataAccess.SaveData("dbo.spManufacturer_Update",
                                        new
                                        {
                                            Id = updatedManufacturer.Id,
                                            Name = updatedManufacturer.Name,
                                        },
                                        _connectionString.SqlConnectionName);
        }
    }
}
