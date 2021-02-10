using BigShop.DataAccess.Db;
using BigShop.Models.Country;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigShop.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private readonly IDataAccess _dataAccess;
        private readonly ConnectionStringData _connectionString;

        public CountryRepository(IDataAccess dataAccess, ConnectionStringData connectionString)
        {
            _dataAccess = dataAccess;
            _connectionString = connectionString;
        }

        public async Task<int> CreateAsync(CountryCreate countryCreate)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("Name", countryCreate.Name);
            p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

            await _dataAccess.SaveData("dbo.spCountry_Insert", p, _connectionString.SqlConnectionName);

            return p.Get<int>("Id");
        }

        public Task<int> DeleteAsync(int countryId)
        {
            return _dataAccess.SaveData("dbo.spCountry_Delete",
                                        new { Id = countryId },
                                        _connectionString.SqlConnectionName);
        }

        public Task<List<Country>> GetAllAsync()
        {
            return _dataAccess.LoadData<Country, dynamic>("dbo.spCountry_GetAll",
                                                          new { },
                                                          _connectionString.SqlConnectionName);
        }

        public async Task<Country> GetByIdAsync(int countryId)
        {
            var recs = await _dataAccess.LoadData<Country, dynamic>("dbo.spCountry_GetById",
                                                              new { Id = countryId },
                                                              _connectionString.SqlConnectionName);
            return recs.FirstOrDefault();
        }

        public Task<int> UpdateAsync(Country updatedCountry)
        {
            return _dataAccess.SaveData("dbo.spCountry_Update",
                                        new
                                        {
                                            Id = updatedCountry.Id,
                                            Name = updatedCountry.Name
                                        },
                                        _connectionString.SqlConnectionName);
        }
    }
}
