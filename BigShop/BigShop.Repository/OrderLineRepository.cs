using BigShop.DataAccess.Db;
using BigShop.Models.OrderLine;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigShop.Repository
{
    public class OrderLineRepository : IOrderLineRepository
    {
        private readonly IDataAccess _dataAccess;
        private readonly ConnectionStringData _connectionString;

        public OrderLineRepository(IDataAccess dataAccess, ConnectionStringData connectionString)
        {
            _dataAccess = dataAccess;
            _connectionString = connectionString;
        }

        public async Task<int> CreateAsync(List<OrderLineCreate> orderLineCreate)
        {
            int rows = 0;

            foreach (var item in orderLineCreate)
            {
                DynamicParameters p = new DynamicParameters();
                p.Add("Quantity", item.Quantity);
                p.Add("Price", item.Price);
                p.Add("Product_Id", item.Product_Id);
                p.Add("Customer_Id", item.CustomerOrder_Id);

                rows += await _dataAccess.SaveData("dbo.spOrderLine_Insert", p, _connectionString.SqlConnectionName);
            }
            return rows;
        }

        public Task<int> DeleteByCustomerOrderIdAsync(int customerOrderId)
        {
            return _dataAccess.SaveData("dbo.spOrderLine_DeleteByCustomerOrderId",
                                        new { Id = customerOrderId },
                                        _connectionString.SqlConnectionName);
        }

        public Task<List<OrderLine>> GetByCustomerOrderIdAsync(int customerOrderId)
        {
            return _dataAccess.LoadData<OrderLine, dynamic>("dbo.spOrderLine_GetByCustomerOrderId",
                                                             new
                                                             {
                                                                 CustomerOrder_Id = customerOrderId
                                                             },
                                                             _connectionString.SqlConnectionName);
        }
    }
}
