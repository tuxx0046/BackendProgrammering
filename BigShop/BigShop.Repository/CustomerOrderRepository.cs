using BigShop.DataAccess.Db;
using BigShop.Models.CustomerOrder;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigShop.Repository
{
    public class CustomerOrderRepository : ICustomerOrderRepository
    {
        private readonly IDataAccess _dataAccess;
        private readonly ConnectionStringData _connectionString;

        public CustomerOrderRepository(IDataAccess dataAccess, ConnectionStringData connectionString)
        {
            _dataAccess = dataAccess;
            _connectionString = connectionString;
        }

        public async Task<int> CreateAsync(CustomerOrderCreate customerOrderCreate)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("InitialShippingCost", customerOrderCreate.InitialShippingCost);
            p.Add("WeightFee", customerOrderCreate.WeightFee);
            p.Add("Customer_Id", customerOrderCreate.Customer_Id);
            p.Add("Courier_Id", customerOrderCreate.Courier_Id);
            p.Add("PaymentMethod_Id", customerOrderCreate.PaymentMethod_Id);
            p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

            await _dataAccess.SaveData("dbo.spCustomerOrder_Insert", p, _connectionString.SqlConnectionName);

            return p.Get<int>("Id");
        }


        public Task<int> DeleteAsync(int customerOrderId)
        {
            return _dataAccess.SaveData("dbo.spCustomerOrder_Delete",
                                        new { Id = customerOrderId },
                                        _connectionString.SqlConnectionName);
        }

        public Task<List<CustomerOrder>> GetAllAsync()
        {
            return _dataAccess.LoadData<CustomerOrder, dynamic>("dbo.spCustomerOrder_GetAll",
                                                                new { },
                                                                _connectionString.SqlConnectionName);
        }

        public Task<List<CustomerOrder>> GetAllByCustomerId(int customerId)
        {
            return _dataAccess.LoadData<CustomerOrder, dynamic>("dbo.spCustomerOrder_GetAllByCustomerId",
                                                                new { Customer_Id = customerId },
                                                                _connectionString.SqlConnectionName)
        }

        public async Task<CustomerOrder> GetByIdAsync(int customerOrderId)
        {
            var recs = await _dataAccess.LoadData<CustomerOrder, dynamic>("dbo.spCustomerOrder_GetById",
                                                                    new { Id = customerOrderId },
                                                                    _connectionString.SqlConnectionName);
            return recs.FirstOrDefault();
        }
    }
}
