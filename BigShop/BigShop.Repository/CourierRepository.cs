using BigShop.DataAccess.Db;
using BigShop.Models.Courier;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigShop.Repository
{
    public class CourierRepository : ICourierRepository
    {
        private readonly IDataAccess _dataAccess;
        private readonly ConnectionStringData _connectionString;

        public CourierRepository(IDataAccess dataAccess, ConnectionStringData connectionString)
        {
            _dataAccess = dataAccess;
            _connectionString = connectionString;
        }

        public async Task<int> CreateAsync(CourierCreate courierCreate)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("Name", courierCreate.Name);
            p.Add("InitialCost", courierCreate.InitialCost);
            p.Add("WeightFee", courierCreate.WeightFee);
            p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

            int affectedRows = await _dataAccess.SaveData("dbo.spCourier_Insert", p, _connectionString.SqlConnectionName);
            if (affectedRows > 0)
            {
                return p.Get<int>("Id");
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// Should not be used, because customer orders need to know the courier used.
        /// </summary>
        /// <param name="courierId"></param>
        /// <returns></returns>
        public Task<int> DeleteAsync(int courierId)
        {
            return _dataAccess.SaveData("dbo.spCourier_Delete",
                                        new { Id = courierId },
                                        _connectionString.SqlConnectionName);
        }

        public Task<List<Courier>> GetAllAsync()
        {
            return _dataAccess.LoadData<Courier, dynamic>("dbo.spCourier_GetAll",
                                                          new { },
                                                          _connectionString.SqlConnectionName);
        }

        public async Task<Courier> GetByIdAsync(int courierId)
        {
            var recs = await _dataAccess.LoadData<Courier, dynamic>("dbo.spCourier_GetById",
                                                              new { Id = courierId },
                                                              _connectionString.SqlConnectionName);
            return recs.FirstOrDefault();
        }

        public Task<int> UpdateAsync(Courier updatedCourier)
        {
            return _dataAccess.SaveData("dbo.spCourier_Update",
                                        new
                                        {
                                            Id = updatedCourier.Id,
                                            Name = updatedCourier.Name,
                                            InitialCost = updatedCourier.InitialCost,
                                            WeightFee = updatedCourier.WeightFee
                                        },
                                        _connectionString.SqlConnectionName);
        }
    }
}