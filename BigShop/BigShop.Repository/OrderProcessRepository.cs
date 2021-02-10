﻿using BigShop.DataAccess.Db;
using BigShop.Models.OrderProcess;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigShop.Repository
{
    public class OrderProcessRepository : IOrderProcessRepository
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

        public Task<int> DeleteByCustomerOrderIdAsync(int customerOrderId)
        {
            return _dataAccess.SaveData("dbo.spOrderProcess_DeleteByCustomerOrderId",
                                        new { CustomerOrder_Id = customerOrderId },
                                        _connectionString.SqlConnectionName);
        }

        public Task<List<OrderProcess>> GetAllAsync()
        {
            return _dataAccess.LoadData<OrderProcess, dynamic>("dbo.spOrderProcess_GetAll",
                                                             new { },
                                                             _connectionString.SqlConnectionName);
        }

        public async Task<OrderProcess> GetByCustomerOrderIdAsync(int customerOrderId)
        {
            var recs = await _dataAccess.LoadData<OrderProcess, dynamic>("dbo.spOrderProcess_GetByCustomerOrderId",
                                                                       new { Id = customerOrderId },
                                                                       _connectionString.SqlConnectionName);
            return recs.FirstOrDefault();
        }
    }
}
