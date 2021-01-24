using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Options;
using SmartAuth.Configurations;
using SmartAuth.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace SmartAuth.Repositories
{
    public class DapperRepository<T> : IRepository<T> where T : class
    {
        private readonly IDbConnection _dbConnection;

        public DapperRepository(IOptions<DatabaseConfiguration> databaseConfig)
        {
            _dbConnection = new SqlConnection(databaseConfig.Value.ConnectionString);
        }

        private IDbConnection CreateConnection()
        {
            var conn = _dbConnection;
            conn.Open();
            return conn;
        }

        public async Task<bool> AddAsync(T entity)
        {
            using var connection = CreateConnection();

            var result = await connection.InsertAsync(entity);

            return result is 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            using var connection = CreateConnection();

            var entity = await connection.GetAsync<T>(id);

            if (entity is null)
                return false;

            var deleted = await connection.DeleteAsync(entity);

            return deleted;
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            using var connection = CreateConnection();

            return await connection.GetAsync<T>(id);
        }

        public async Task<List<T>> ListAsync()
        {
            using var connection = CreateConnection();

            return (List<T>)await connection.GetAllAsync<T>();
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            using var connection = CreateConnection();

            var updated = await connection.UpdateAsync(entity);

            return updated;
        }
    }
}
