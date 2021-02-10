using BigShop.DataAccess.Db;
using BigShop.Models.Employee;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigShop.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IDataAccess _dataAccess;
        private readonly ConnectionStringData _connectionString;

        public EmployeeRepository(IDataAccess dataAccess, ConnectionStringData connectionString)
        {
            _dataAccess = dataAccess;
            _connectionString = connectionString;
        }

        public async Task<int> CreateAsync(EmployeeCreate employeeCreate)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("FirstName", employeeCreate.FirstName);
            p.Add("LastName", employeeCreate.LastName);
            p.Add("Phone", employeeCreate.Phone);
            p.Add("Position_Id", employeeCreate.Position_Id);
            p.Add("Department_Id", employeeCreate.Department_Id);
            p.Add("ApplicationUser_Id", employeeCreate.ApplicationUser_Id);
            p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

            await _dataAccess.SaveData("dbo.spEmployee_Insert", p, _connectionString.SqlConnectionName);

            return p.Get<int>("Id");
        }

        public Task<int> DeleteAsync(int employeeId)
        {
            return _dataAccess.SaveData("dbo.spEmployee_Delete",
                                        new { Id = employeeId },
                                        _connectionString.SqlConnectionName);
        }

        public Task<List<Employee>> GetAllAsync()
        {
            return _dataAccess.LoadData<Employee, dynamic>("dbo.spEmployee_GetAll",
                                                             new { },
                                                             _connectionString.SqlConnectionName);
        }

        public async Task<Employee> GetByIdAsync(int employeeId)
        {
            var recs = await _dataAccess.LoadData<Employee, dynamic>("dbo.spEmployee_GetById",
                                                                       new { Id = employeeId },
                                                                       _connectionString.SqlConnectionName);
            return recs.FirstOrDefault();
        }

        public Task<int> UpdateAsync(Employee updatedEmployee)
        {
            return _dataAccess.SaveData("dbo.spEmployee_Update",
                                        new
                                        {
                                            Id = updatedEmployee.Id,
                                            FirstName = updatedEmployee.FirstName,
                                            LastName = updatedEmployee.LastName,
                                            Phone = updatedEmployee.Phone,
                                            Position_Id = updatedEmployee.Position_Id,
                                            Department_Id = updatedEmployee.Department_Id
                                        },
                                        _connectionString.SqlConnectionName);
        }
    }
}