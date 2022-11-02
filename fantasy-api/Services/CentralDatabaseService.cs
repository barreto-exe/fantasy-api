using FantasyApi.Interfaces;
using MySqlConnector;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace FantasyApi.Services
{
    public class CentralDatabaseService : IBaseDatabaseService
    {
        public string Connectionstring { get; set; }

        public CentralDatabaseService(string _connectionstring)
        {
            Connectionstring = _connectionstring;
        }

        public void SetConnectionString(string _connectionstring)
        {
            Connectionstring = _connectionstring;
        }

        private MySqlConnection GetConnection()
        {
            MySqlConnection con = new(Connectionstring);
            return con;
        }

        public DataTable ExecuteStoredProcedure(MySqlCommand cmd)
        {
            DataTable dataTable = new();
            using (var con = GetConnection())
            {
                cmd.Connection = con;
                cmd.Connection.Open();
                MySqlDataAdapter da = new(cmd);
                da.Fill(dataTable);
                cmd.Connection.Close();
                da.Dispose();
            }
            return dataTable;
        }

        public async Task<DataTable> ExecuteStoredProcedureAsync(MySqlCommand cmd)
        {
            DataTable dataTable = new();
            int rowsAffected = 0;
            using (var con = GetConnection())
            {
                cmd.Connection = con;
                await cmd.Connection.OpenAsync();
                MySqlDataAdapter da = new(cmd);
                rowsAffected = da.Fill(dataTable);
                await cmd.Connection.CloseAsync();
                da.Dispose();
            }
            return dataTable;
        }

        public async Task<DataSet> ExecuteStoredProcedureDataSetAsync(MySqlCommand cmd)
        {
            DataSet set = new();
            int rowsAffected = 0;
            using (var con = GetConnection())
            {
                cmd.Connection = con;
                await cmd.Connection.OpenAsync();
                MySqlDataAdapter da = new(cmd);
                rowsAffected = da.Fill(set);
                await cmd.Connection.CloseAsync();
                da.Dispose();
            }
            return set;
        }

        public async Task<int> ExecuteNonQuerySpAsync(MySqlCommand cmd)
        {
            int rowsAffected = 0;
            using (var con = GetConnection())
            {
                cmd.Connection = con;
                await cmd.Connection.OpenAsync();
                rowsAffected = cmd.ExecuteNonQuery();
                await cmd.Connection.CloseAsync();
            }
            return rowsAffected;
        }

        public MySqlCommand GetCommand(string spname, List<MySqlParameter>? parameters = null, CommandType type = CommandType.StoredProcedure)
        {
            MySqlCommand cmd = new(spname)
            {
                CommandType = type,
            };

            if (parameters != null && parameters.Count > 0)
            {
                foreach (var param in parameters)
                {
                    cmd.Parameters.Add(param);
                }
            }

            return cmd;
        }
    }
}