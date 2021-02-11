using BigShop.DataAccess.Db;
using BigShop.Models.Position;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigShop.Repository
{
    public class PositionRepository : IPositionRepository
    {
        private readonly IDataAccess _dataAccess;
        private readonly ConnectionStringData _connectionString;

        public PositionRepository(IDataAccess dataAccess, ConnectionStringData connectionString)
        {
            _dataAccess = dataAccess;
            _connectionString = connectionString;
        }

        public async Task<int> CreateAsync(PositionCreate positionCreate)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("Name", positionCreate.Name);
            p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

            await _dataAccess.SaveData("dbo.spPosition_Insert", p, _connectionString.SqlConnectionName);

            return p.Get<int>("Id");
        }

        public Task<int> DeleteAsync(int positionId)
        {
            return _dataAccess.SaveData("dbo.spPosition_Delete",
                                        new { Id = positionId },
                                        _connectionString.SqlConnectionName);
        }

        public Task<List<Position>> GetAllAsync()
        {
            return _dataAccess.LoadData<Position, dynamic>("dbo.spPosition_GetAll",
                                                             new { },
                                                             _connectionString.SqlConnectionName);
        }

        public async Task<Position> GetByIdAsync(int paymentMethodId)
        {
            var recs = await _dataAccess.LoadData<Position, dynamic>("dbo.spPosition_GetById",
                                                                       new { Id = paymentMethodId },
                                                                       _connectionString.SqlConnectionName);
            return recs.FirstOrDefault();
        }

        public Task<int> UpdateAsync(Position updatedPosition)
        {
            return _dataAccess.SaveData("dbo.spPosition_Update",
                                        new
                                        {
                                            Id = updatedPosition.Id,
                                            Name = updatedPosition.Name
                                        },
                                        _connectionString.SqlConnectionName);
        }
    }
}
