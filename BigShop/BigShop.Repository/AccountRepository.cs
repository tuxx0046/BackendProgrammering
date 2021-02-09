using BigShop.DataAccess.Db;
using BigShop.Models.Account;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BigShop.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IDataAccess _dataAccess;
        private readonly ConnectionStringData _connectionString;
        private readonly IConfiguration _config;

        public AccountRepository(IDataAccess dataAccess, ConnectionStringData connectionString, IConfiguration config)
        {
            _dataAccess = dataAccess;
            _connectionString = connectionString;
            _config = config;
        }

        public async Task<IdentityResult> CreateAsync(ApplicationUserIdentity user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            /*
            var dataTable = new DataTable();
            dataTable.Columns.Add("Username", typeof(string));
            dataTable.Columns.Add("NormalizedUsername", typeof(string));
            dataTable.Columns.Add("Email", typeof(string));
            dataTable.Columns.Add("NormalizedEmail", typeof(string));
            dataTable.Columns.Add("Fullname", typeof(string));
            dataTable.Columns.Add("PasswordHash", typeof(string));

            dataTable.Rows.Add(
                    user.Username,
                    user.NormalizedUsername,
                    user.Email,
                    user.NormalizedEmail,
                    user.PasswordHash
            );
            */

            DynamicParameters p = new DynamicParameters();

            p.Add("Username", user.Username);
            p.Add("NormalizedUsername", user.NormalizedUsername);
            p.Add("Email", user.Email);
            p.Add("NormalizedEmail", user.NormalizedEmail);
            p.Add("PasswordHash", user.PasswordHash);
            p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

            using (var connection = new SqlConnection(_config.GetConnectionString(_connectionString.SqlConnectionName)))
            {
                await connection.OpenAsync(cancellationToken);

                await connection.ExecuteScalarAsync("dbo.spApplicationUser_Insert",
                                                    p,
                                                    commandType: CommandType.StoredProcedure);
            }

            return IdentityResult.Success;
        }

        public async Task<ApplicationUserIdentity> GetById(int id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ApplicationUserIdentity applicationUser;

            using (var connection = new SqlConnection(_config.GetConnectionString(_connectionString.SqlConnectionName)))
            {
                await connection.OpenAsync(cancellationToken);

                applicationUser = await connection.QuerySingleOrDefaultAsync<ApplicationUserIdentity>(
                    "spApplicationUser_GetById",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure);
            }
            return applicationUser;
        }

        public async Task<ApplicationUserIdentity> GetByUsernameAsync(string normalizedUsername, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ApplicationUserIdentity applicationUser;

            using (var connection = new SqlConnection(_config.GetConnectionString(_connectionString.SqlConnectionName)))
            {
                await connection.OpenAsync(cancellationToken);

                applicationUser = await connection.QuerySingleOrDefaultAsync<ApplicationUserIdentity>(
                    "spApplicationUser_GetByUsername",
                    new { NormalizedUsername = normalizedUsername },
                    commandType: CommandType.StoredProcedure);
            }
            return applicationUser;
        }
    }
}
