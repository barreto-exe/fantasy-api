using MySqlConnector;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace FantasyApi.Interfaces
{
    public interface IBaseDatabaseService
    {
        DataTable ExecuteStoredProcedure(MySqlCommand cmd);
        Task<DataTable> ExecuteStoredProcedureAsync(MySqlCommand cmd);
        Task<DataSet> ExecuteStoredProcedureDataSetAsync(MySqlCommand cmd);
        Task<int> ExecuteNonQuerySpAsync(MySqlCommand cmd);
        MySqlCommand GetCommand(string spname, List<MySqlParameter> parameters = null, CommandType type = CommandType.StoredProcedure);
    }
}