using BigShop.DataAccess.Db;
using BigShop.Models.Zip;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigShop.Repository
{
    public class ZipRepository : IZipRepository
    {
        private readonly IDataAccess _dataAccess;
        private readonly ConnectionStringData _connectionString;

        public ZipRepository(IDataAccess dataAccess, ConnectionStringData connectionString)
        {
            _dataAccess = dataAccess;
            _connectionString = connectionString;
        }

        public async Task<int> CreateAsync(ZipCreate zipCreate)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("ZipCode", zipCreate.ZipCode);
            p.Add("CityName", zipCreate.CityName);
            p.Add("Country_Id", zipCreate.Country_Id);
            p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

            await _dataAccess.SaveData("dbo.spZip_Insert", p, _connectionString.SqlConnectionName);

            return p.Get<int>("Id");
        }

        public Task<int> DeleteAsync(int zipId)
        {
            return _dataAccess.SaveData("dbo.spZip_Delete",
                                        new { Id = zipId },
                                        _connectionString.SqlConnectionName);
        }

        public Task<List<Zip>> GetByCountryIdAsync(int countryId)
        {
            return _dataAccess.LoadData<Zip, dynamic>("dbo.spZip_GetByCountryId",
                                                             new { Country_Id = countryId },
                                                             _connectionString.SqlConnectionName);
        }

        public async Task<Zip> GetByIdAsync(int zipId)
        {
            var recs = await _dataAccess.LoadData<Zip, dynamic>("dbo.spZip_GetById",
                                                                       new { Id = zipId },
                                                                       _connectionString.SqlConnectionName);
            return recs.FirstOrDefault();
        }

        public Task<int> UpdateAsync(Zip updatedZip)
        {
            return _dataAccess.SaveData("dbo.spZip_Update",
                                        updatedZip,
                                        _connectionString.SqlConnectionName);
        }
    }
}
