using Core.Utils.Mapping;
using FantasyApi.Data.Base.Dtos;
using FantasyApi.Data.Base.Requests;
using FantasyApi.Interfaces;
using FantasyApi.Utils;
using MySqlConnector;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FantasyApi.Services
{
    public abstract class BaseService
    {
        protected readonly IDatabaseService _databaseService;
        protected BaseService(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        protected async Task<PaginatedListDto<T>> GetItemsPaginatedAsync<T>(BaseRequest filter, string spName) where T : class, new()
        {
            List<MySqlParameter> parameters = new()
            {
                new MySqlParameter("page", filter.Page),
                new MySqlParameter("size", filter.Size),
            };

            var cmd = _databaseService.GetCommand(spName, parameters);
            var data = await _databaseService.ExecuteStoredProcedureDataSetAsync(cmd);

            if (data.Tables[0].Rows.Count > 0)
            {
                var mapper = new DataNamesMapper<T>();
                var users = mapper.Map(data.Tables[0]);

                int totalRows = (int)data.Tables[1].Rows[0]["totalRows"];
                int page = (int)data.Tables[1].Rows[0]["page"];
                int totalPages = (int)data.Tables[1].Rows[0]["totalPages"];
                int size = (int)data.Tables[1].Rows[0]["size"];

                return ResponsesBuilder.PaginatedListResponse(users, totalRows, page, totalPages, size);
            }
            else
            {
                return null;
            }
        }

        protected async Task<IEnumerable<T>> GetItemsAsync<T>(string spName) where T : class, new()
        {
            var cmd = _databaseService.GetCommand(spName);
            var data = await _databaseService.ExecuteStoredProcedureAsync(cmd);

            if (data.Rows.Count > 0)
            {
                var mapper = new DataNamesMapper<T>();
                var items = mapper.Map(data);
                return items;
            }
            else
            {
                return null;
            }
        }

        protected async Task<T> GetItemByIdAsync<T>(string spName, string spVarName, int id) where T : class, new()
        {
            List<MySqlParameter> parameters = new()
            {
                new MySqlParameter(spVarName, id),
            };

            var cmd = _databaseService.GetCommand(spName, parameters);
            var data = await _databaseService.ExecuteStoredProcedureAsync(cmd);

            if (data.Rows.Count > 0)
            {
                var mapper = new DataNamesMapper<T>();
                var item = mapper.Map(data.Rows[0]);
                return item;
            }
            else
            {
                return null;
            }
        }

        public async Task DeleteItemAsync(string spName, string spVarName, int id)
        {
            List<MySqlParameter> parameters = new()
            {
                new MySqlParameter(spVarName, id),
            };

            var cmd = _databaseService.GetCommand(spName, parameters);
            await _databaseService.ExecuteStoredProcedureAsync(cmd);
        }
    }
}
