using BigShop.DataAccess.Db;
using BigShop.Models.Department;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigShop.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly IDataAccess _dataAccess;
        private readonly ConnectionStringData _connectionString;

        public DepartmentRepository(IDataAccess dataAccess, ConnectionStringData connectionString)
        {
            _dataAccess = dataAccess;
            _connectionString = connectionString;
        }

        public async Task<int> CreateAsync(DepartmentCreate departmentCreate)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("Name", departmentCreate.Name);
            p.Add("Phone", departmentCreate.Phone);
            p.Add("Warehouse_Id", departmentCreate.Warehouse_Id);
            p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

            await _dataAccess.SaveData("dbo.spDepartment_Insert", p, _connectionString.SqlConnectionName);

            return p.Get<int>("Id");
        }

        public Task<int> DeleteAsync(int departmentId)
        {
            return _dataAccess.SaveData("dbo.spDepartment_Delete",
                                        new { Id = departmentId },
                                        _connectionString.SqlConnectionName);
        }

        public Task<List<Department>> GetAllAsync()
        {
            return _dataAccess.LoadData<Department, dynamic>("dbo.spDepartment_GetAll",
                                                             new { },
                                                             _connectionString.SqlConnectionName);
        }

        public async Task<Department> GetByIdAsync(int departmentId)
        {
            var recs = await _dataAccess.LoadData<Department, dynamic>("dbo.spDepartment_GetById",
                                                                       new { Id = departmentId },
                                                                       _connectionString.SqlConnectionName);
            return recs.FirstOrDefault();
        }

        public Task<int> UpdateAsync(Department updatedDepartment)
        {
            return _dataAccess.SaveData("dbo.spDepartment_Update",
                                        new
                                        {
                                            Id = updatedDepartment.Id,
                                            Name = updatedDepartment.Name,
                                            Phone = updatedDepartment.Phone,
                                            Warehouse_Id = updatedDepartment.Id
                                        },
                                        _connectionString.SqlConnectionName);
        }
    }
}