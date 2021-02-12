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

        /// <summary>
        /// Create new category
        /// </summary>
        /// <param name="categoryCreate"></param>
        /// <returns>Id if successful. -1 if no rows were affected</returns>
        public async Task<int> CreateAsync(CategoryCreate categoryCreate)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("Name", categoryCreate.Name);
            p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

            int affectedRows = await _dataAccess.SaveData("dbo.spCategory_Insert", p, _connectionString.SqlConnectionName);
            if (affectedRows > 0)
            {
                return p.Get<int>("Id");
            }
            else
            {
                return -1;
            }
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
