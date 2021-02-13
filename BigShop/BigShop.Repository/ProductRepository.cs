using BigShop.DataAccess.Db;
using BigShop.Models.Product;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigShop.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDataAccess _dataAccess;
        private readonly ConnectionStringData _connectionString;

        public ProductRepository(IDataAccess dataAccess, ConnectionStringData connectionString)
        {
            _dataAccess = dataAccess;
            _connectionString = connectionString;
        }

        public async Task<int> CreateAsync(ProductCreate productCreate)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("Name", productCreate.Name);
            p.Add("Price", productCreate.Price);
            p.Add("EAN", productCreate.EAN);
            p.Add("WeightGram", productCreate.WeightGram);
            p.Add("Manufacturer_Id", productCreate.Manufacturer_Id);
            p.Add("Category_Id", productCreate.Category_Id);
            p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

            int affectedRows = await _dataAccess.SaveData("dbo.spProduct_Insert", p, _connectionString.SqlConnectionName);
            if (affectedRows > 0)
            {
                return p.Get<int>("Id");
            }
            else
            {
                return -1;
            }
        }

        public Task<int> DeleteAsync(int productId)
        {
            return _dataAccess.SaveData("dbo.spProduct_Delete",
                                        new { Id = productId },
                                        _connectionString.SqlConnectionName);
        }

        public Task<List<Product>> GetAllAsync()
        {
            return _dataAccess.LoadData<Product, dynamic>("dbo.spProduct_GetAll",
                                                             new { },
                                                             _connectionString.SqlConnectionName);
        }

        public Task<List<Product>> GetByCategoryIdAsync(int categoryId)
        {
            return _dataAccess.LoadData<Product, dynamic>("dbo.spProduct_GetByCategoryId",
                                                          new { Category_Id = categoryId },
                                                          _connectionString.SqlConnectionName);
        }

        public async Task<Product> GetByIdAsync(int productId)
        {
            var recs = await _dataAccess.LoadData<Product, dynamic>("dbo.spProduct_GetById",
                                                                       new { Id = productId },
                                                                       _connectionString.SqlConnectionName);
            return recs.FirstOrDefault();
        }

        public Task<List<Product>> GetByManufacturerIdAsync(int manufacturerId)
        {
            return _dataAccess.LoadData<Product, dynamic>("dbo.spProduct_GetByManufacturerId",
                                                          new { Manufacturer_Id = manufacturerId },
                                                          _connectionString.SqlConnectionName);
        }

        public Task<int> UpdateAsync(Product updatedProduct)
        {
            return _dataAccess.SaveData("dbo.spProduct_Update",
                                        new
                                        {
                                            Id = updatedProduct.Id,
                                            Name = updatedProduct.Name,
                                            Price = updatedProduct.Price,
                                            EAN = updatedProduct.EAN,
                                            WeightGram = updatedProduct.WeightGram,
                                            Manufacturer_Id = updatedProduct.Manufacturer_Id,
                                            Category_Id = updatedProduct.Category_Id
                                        },
                                        _connectionString.SqlConnectionName);
        }
    }
}