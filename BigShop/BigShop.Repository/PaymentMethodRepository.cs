using BigShop.DataAccess.Db;
using BigShop.Models.PaymentMethod;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigShop.Repository
{
    public class PaymentMethodRepository : IPaymentMethodRepository
    {
        private readonly IDataAccess _dataAccess;
        private readonly ConnectionStringData _connectionString;

        public PaymentMethodRepository(IDataAccess dataAccess, ConnectionStringData connectionString)
        {
            _dataAccess = dataAccess;
            _connectionString = connectionString;
        }

        public async Task<int> CreateAsync(PaymentMethodCreate paymentMethodCreate)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("Name", paymentMethodCreate.Name);
            p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

            await _dataAccess.SaveData("dbo.spPaymentMethod_Insert", p, _connectionString.SqlConnectionName);

            return p.Get<int>("Id");
        }

        public Task<int> DeleteAsync(int paymentMethodId)
        {
            return _dataAccess.SaveData("dbo.spPaymentMethod_Delete",
                                        new { Id = paymentMethodId },
                                        _connectionString.SqlConnectionName);
        }

        public Task<List<PaymentMethod>> GetAllAsync()
        {
            return _dataAccess.LoadData<PaymentMethod, dynamic>("dbo.spPaymentMethod_GetAll",
                                                             new { },
                                                             _connectionString.SqlConnectionName);
        }

        public async Task<PaymentMethod> GetByIdAsync(int paymentMethodId)
        {
            var recs = await _dataAccess.LoadData<PaymentMethod, dynamic>("dbo.spPaymentMethod_GetById",
                                                                       new { Id = paymentMethodId },
                                                                       _connectionString.SqlConnectionName);
            return recs.FirstOrDefault();
        }

        public Task<int> UpdateAsync(PaymentMethod updatedPaymentMethod)
        {
            return _dataAccess.SaveData("dbo.spPaymentMethod_Update",
                                        new
                                        {
                                            Id = updatedPaymentMethod.Id,
                                            Name = updatedPaymentMethod.Name
                                        },
                                        _connectionString.SqlConnectionName);
        }
    }
}
