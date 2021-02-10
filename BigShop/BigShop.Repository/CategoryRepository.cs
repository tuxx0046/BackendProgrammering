using BigShop.DataAccess.Db;
using BigShop.Models.Category;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigShop.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IDataAccess _dataAccess;
        private readonly ConnectionStringData _connectionString;

        public CategoryRepository(IDataAccess dataAccess, ConnectionStringData connectionString)
        {
            _dataAccess = dataAccess;
            _connectionString = connectionString;
        }

        public async Task<int> CreateAsync(CategoryCreate categoryCreate)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("Name", categoryCreate.Name);
            p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

            await _dataAccess.SaveData("dbo.spCategory_Insert", p, _connectionString.SqlConnectionName);

            return p.Get<int>("Id");
        }

        public Task<int> DeleteAsync(int categoryId)
        {
            return _dataAccess.SaveData("dbo.spCategory_Delete",
                                        new
                                        {
                                            Id = categoryId
                                        },
                                        _connectionString.SqlConnectionName);
        }

        public Task<List<Category>> GetAllAsync()
        {
            return _dataAccess.LoadData<Category, dynamic>("spCategory_GetAll",
                                                           new { },
                                                           _connectionString.SqlConnectionName);
        }

        public async Task<Category> GetByIdAsync(int categoryId)
        {
            var recs = await _dataAccess.LoadData<Category, dynamic>("spCategory_GetById",
                                                                     new { Id = categoryId },
                                                                     _connectionString.SqlConnectionName);
            return recs.FirstOrDefault();
        }

        public Task<int> UpdateAsync(Category updatedCategory)
        {
            return _dataAccess.SaveData("dbo.spCategory_Update",
                                        new 
                                        { 
                                            Id = updatedCategory.Id, 
                                            Name = updatedCategory.Name
                                        },
                                        _connectionString.SqlConnectionName);
        }
    }
}
