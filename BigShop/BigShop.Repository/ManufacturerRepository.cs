using BigShop.DataAccess.Db;
using BigShop.Models.Manufacturer;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigShop.Repository
{
    public class ManufacturerRepository : IManufacturerRepository
    {
        private readonly IDataAccess _dataAccess;
        private readonly ConnectionStringData _connectionString;

        public ManufacturerRepository(IDataAccess dataAccess, ConnectionStringData connectionString)
        {
            _dataAccess = dataAccess;
            _connectionString = connectionString;
        }

        public async Task<int> CreateAsync(ManufacturerCreate manufacturerCreate)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("Name", manufacturerCreate.Name);
            p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

            await _dataAccess.SaveData("dbo.spManufacturer_Insert", p, _connectionString.SqlConnectionName);

            return p.Get<int>("Id");
        }

        public Task<int> DeleteAsync(int manufacturerId)
        {
            return _dataAccess.SaveData("dbo.spManufacturer_Delete",
                                        new { Id = manufacturerId },
                                        _connectionString.SqlConnectionName);
        }

        public Task<List<Manufacturer>> GetAllAsync()
        {
            return _dataAccess.LoadData<Manufacturer, dynamic>("dbo.spManufacturer_GetAll",
                                                             new { },
                                                             _connectionString.SqlConnectionName);
        }

        public async Task<Manufacturer> GetByIdAsync(int manufacturerId)
        {
            var recs = await _dataAccess.LoadData<Manufacturer, dynamic>("dbo.spManufacturer_GetById",
                                                                       new { Id = manufacturerId },
                                                                       _connectionString.SqlConnectionName);
            return recs.FirstOrDefault();
        }

        public Task<int> UpdateAsync(Manufacturer updatedManufacturer)
        {
            return _dataAccess.SaveData("dbo.spManufacturer_Update",
                                        new
                                        {
                                            Id = updatedManufacturer.Id,
                                            Name = updatedManufacturer.Name
                                        },
                                        _connectionString.SqlConnectionName);
        }
    }
}
