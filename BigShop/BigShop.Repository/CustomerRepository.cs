using BigShop.DataAccess.Db;
using BigShop.Models.Customer;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigShop.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IDataAccess _dataAccess;
        private readonly ConnectionStringData _connectionString;

        public CustomerRepository(IDataAccess dataAccess, ConnectionStringData connectionString)
        {
            _dataAccess = dataAccess;
            _connectionString = connectionString;
        }

        public async Task<int> CreateAsync(CustomerCreate customerCreate)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("FirstName", customerCreate.FirstName);
            p.Add("LastName", customerCreate.LastName);
            p.Add("Phone", customerCreate.Phone);
            p.Add("AddressLane", customerCreate.AddressLane);
            p.Add("Zip_Id", customerCreate.Zip_Id);
            //p.Add("ApplicationUser_Id", customerCreate.ApplicationUser_Id);
            p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

            int affectedRows = await _dataAccess.SaveData("dbo.spCustomer_Insert", p, _connectionString.SqlConnectionName);
            if (affectedRows > 0)
            {
                return p.Get<int>("Id");
            }
            else
            {
                return -1;
            }
        }

        public Task<int> DeleteAsync(int customerId)
        {
            return _dataAccess.SaveData("dbo.spCustomer_Delete",
                                        new { Id = customerId },
                                        _connectionString.SqlConnectionName);
        }

        public Task<List<Customer>> GetAllAsync()
        {
            return _dataAccess.LoadData<Customer, dynamic>("dbo.spCustomer_GetAll",
                                                           new { },
                                                           _connectionString.SqlConnectionName);
        }

        //public async Task<Customer> GetByApplicationUserIdAsync(int applicationUserId)
        //{
        //    var recs = await _dataAccess.LoadData<Customer, dynamic>("dbo.spCustomer_GetByApplicationUserId",
        //                                                             new { ApplicationUser_Id = applicationUserId },
        //                                                             _connectionString.SqlConnectionName);

        //    return recs.FirstOrDefault();
        //}

        public async Task<Customer> GetByIdAsync(int customerId)
        {
            var recs = await _dataAccess.LoadData<Customer, dynamic>("dbo.spCustomer_GetById", new { Id = customerId }, _connectionString.SqlConnectionName);

            return recs.FirstOrDefault();
        }

        public Task<int> UpdateAsync(Customer updatedCustomer)
        {
            return _dataAccess.SaveData("dbo.spCustomer_Update",
                                        new
                                        {
                                            Id = updatedCustomer.Id,
                                            FirstName = updatedCustomer.FirstName,
                                            LastName = updatedCustomer.LastName,
                                            Phone = updatedCustomer.Phone,
                                            AddressLane = updatedCustomer.AddressLane,
                                            Zip_Id = updatedCustomer.Zip_Id
                                        },
                                        _connectionString.SqlConnectionName);
        }
    }
}
