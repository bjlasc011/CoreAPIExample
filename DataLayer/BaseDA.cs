using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public abstract class BaseDA<T>
    {
        private readonly string _connectionString;
        private readonly string _dbName;

        public BaseDA(string connectionString, string dbName)
        {
            _dbName = dbName;
            _connectionString = connectionString;
        }

        public IDbConnection SqlConnection { 
            get 
            { 
                return new SqlConnection(string.Format(_connectionString, _dbName));
            } 
        }

        public async Task<IEnumerable<T>> GetListAsync(object param = null)
        {
            using (var con = SqlConnection)
            {
                try
                {
                    con.Open();

                    var data = await con.GetListAsync<T>(param);
                    return data;
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    con.Close();
                }
            }
        }
    }
}
