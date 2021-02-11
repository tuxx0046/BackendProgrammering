using BigShop.DataAccess.Db;
using BigShop.Models.OrderStatus;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigShop.Repository
{
    public class OrderStatusRepository : IOrderStatusRepository
    {
        private readonly IDataAccess _dataAccess;
        private readonly ConnectionStringData _connectionString;

        public OrderStatusRepository(IDataAccess dataAccess, ConnectionStringData connectionString)
        {
            _dataAccess = dataAccess;
            _connectionString = connectionString;
        }

        public async Task<int> CreateAsync(OrderStatusCreate orderStatusCreate)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("Name", orderStatusCreate.Name);
            p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

            await _dataAccess.SaveData("dbo.spOrderStatus_Insert", p, _connectionString.SqlConnectionName);

            return p.Get<int>("Id");
        }

        public Task<int> DeleteAsync(int orderStatusId)
        {
            return _dataAccess.SaveData("dbo.spOrderStatus_Delete",
                                        new { Id = orderStatusId },
                                        _connectionString.SqlConnectionName);
        }

        public Task<List<OrderStatus>> GetAllAsync()
        {
            return _dataAccess.LoadData<OrderStatus, dynamic>("dbo.spOrderStatus_GetAll",
                                                             new { },
                                                             _connectionString.SqlConnectionName);
        }

        public async Task<OrderStatus> GetByIdAsync(int orderStatusId)
        {
            var recs = await _dataAccess.LoadData<OrderStatus, dynamic>("dbo.spOrderStatus_GetById",
                                                                       new { Id = orderStatusId },
                                                                       _connectionString.SqlConnectionName);
            return recs.FirstOrDefault();
        }

        public Task<int> UpdateAsync(OrderStatus updatedOrderStatus)
        {
            return _dataAccess.SaveData("dbo.spOrderStatus_Update",
                                        new
                                        {
                                            Id = updatedOrderStatus.Id,
                                            Name = updatedOrderStatus.Name
                                        },
                                        _connectionString.SqlConnectionName);
        }
    }
}
