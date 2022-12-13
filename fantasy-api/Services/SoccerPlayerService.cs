using FantasyApi.Data.Base.Exceptions;
using FantasyApi.Data.SoccerPlayer.Dtos;
using FantasyApi.Data.SoccerPlayer.Inputs;
using FantasyApi.Interfaces;
using MySqlConnector;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FantasyApi.Services
{
    public class SoccerPlayerService : BaseService, ISoccerPlayerService
    {
        public SoccerPlayerService(IDatabaseService databaseService) : base(databaseService)
        {
        }

        public async Task<IEnumerable<SoccerPlayerDto>> GetPlayersAsync()
        {
            return await GetItemsAsync<SoccerPlayerDto>("GetSoccerPlayers");
        }
        public async Task<SoccerPlayerDto> GetSoccerPlayerByIdAsync(int id)
        {
            return await GetItemByIdAsync<SoccerPlayerDto>("GetSoccerPlayerById", "playerId", id);
        }
        public async Task<SoccerPlayerDto> GetSoccerPlayerByNameAsync(string name)
        {
            return await GetItemByIdAsync<SoccerPlayerDto>("GetSoccerPlayerByName", "playerName", name);
        }
        public async Task<SoccerPlayerDto> GetOrAddPlayerAsync(SoccerPlayerAddInput input)
        {
            return await GetSoccerPlayerByNameAsync(input.Name) ?? await AddSoccerPlayerAsync(input);
        }

        /// <exception cref="AlreadyExistsException"></exception>
        public async Task<SoccerPlayerDto> AddSoccerPlayerAsync(SoccerPlayerAddInput input)
        {
            var playerWithName = await GetSoccerPlayerByNameAsync(input.Name);
            if (playerWithName != null)
            {
                throw new AlreadyExistsException("Player with the requested name");
            }

            List<MySqlParameter> parameters = new()
            {
                new MySqlParameter("p_name", input.Name),
                new MySqlParameter("p_id_external", input.ExternalUuid),
            };

            var cmd = _databaseService.GetCommand("AddSoccerPlayer", parameters);
            await _databaseService.ExecuteStoredProcedureAsync(cmd);

            return await GetSoccerPlayerByNameAsync(input.Name);
        }

        /// <exception cref="NotFoundException"></exception>
        public async Task<SoccerPlayerDto> UpdateSoccerPlayerAsync(SoccerPlayerUpdateInput input)
        {
            var player = await GetSoccerPlayerByNameAsync(input.Name);
            if (player == null)
            {
                throw new NotFoundException("Player with the requested name");
            }

            List<MySqlParameter> parameters = new()
            {
                new MySqlParameter("p_id", input.Id),
                new MySqlParameter("p_name", input.Name ?? player.Name),
                new MySqlParameter("p_id_external", input.ExternalUuid ?? player.ExternalUuid),
            };

            var cmd = _databaseService.GetCommand("UpdateSoccerPlayer", parameters);
            await _databaseService.ExecuteStoredProcedureAsync(cmd);

            return await GetSoccerPlayerByIdAsync(input.Id);
        }
    }
}