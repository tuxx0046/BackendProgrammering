using BigShop.DataAccess.Db;
using BigShop.Models.OrderLine;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigShop.Repository
{
    public class OrderLineRepository
    {
        private readonly IDataAccess _dataAccess;
        private readonly ConnectionStringData _connectionString;

        public OrderLineRepository(IDataAccess dataAccess, ConnectionStringData connectionString)
        {
            _dataAccess = dataAccess;
            _connectionString = connectionString;
        }

        public void CreateAsync(List<OrderLineCreate> orderLineCreate)
        {
            foreach (var item in orderLineCreate)
            {
                DynamicParameters p = new DynamicParameters();
                p.Add("Quantity", item.Quantity);
                p.Add("Price", item.Price);
                p.Add("Product_Id", item.Product_Id);
                p.Add("Customer_Id", item.CustomerOrder_Id);

                _dataAccess.SaveData("dbo.spOrderLine_Insert", p, _connectionString.SqlConnectionName);
            }
        }

        public Task<int> DeleteByCustomerOrderIdAsync(int customerOrderId)
        {
            return _dataAccess.SaveData("dbo.spOrderLine_DeleteByCustomerOrderId",
                                        new { Id = customerOrderId },
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
