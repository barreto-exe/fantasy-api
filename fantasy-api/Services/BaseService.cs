using Core.Utils.Mapping;
using FantasyApi.Data.Base.Dtos;
using FantasyApi.Data.Base.Requests;
using FantasyApi.Data.Events.Dtos;
using FantasyApi.Interfaces;
using FantasyApi.Utils;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        protected async Task<PaginatedListDto<T>> GetItemsPaginated<T>(BaseRequest filter, string spName) where T : class, new()
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
    }
}
