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
        public async Task<SoccerPlayerDto> GetOrAddPlayerAsync(string name)
        {
            return await GetSoccerPlayerByNameAsync(name) ?? await AddSoccerPlayerAsync(name);
        }

        /// <exception cref="AlreadyExistsException"></exception>
        public async Task<SoccerPlayerDto> AddSoccerPlayerAsync(string name)
        {
            var playerWithName = await GetSoccerPlayerByNameAsync(name);
            if (playerWithName != null)
            {
                throw new AlreadyExistsException("Player with the requested name");
            }

            List<MySqlParameter> parameters = new()
            {
                new MySqlParameter("playerName", name),
            };

            var cmd = _databaseService.GetCommand("AddSoccerPlayer", parameters);
            await _databaseService.ExecuteStoredProcedureAsync(cmd);

            return await GetSoccerPlayerByNameAsync(name);
        }

        /// <exception cref="NotFoundException"></exception>
        public async Task<SoccerPlayerDto> UpdateSoccerPlayerAsync(SoccerPlayerUpdateInput input)
        {
            var player = await GetSoccerPlayerByNameAsync(input.OldName);
            if (player == null)
            {
                throw new NotFoundException("Player with the requested name");
            }

            List<MySqlParameter> parameters = new()
            {
                new MySqlParameter("oldName", input.OldName),
                new MySqlParameter("newName", input.NewName),
            };

            var cmd = _databaseService.GetCommand("UpdateSoccerPlayer", parameters);
            await _databaseService.ExecuteStoredProcedureAsync(cmd);

            return await GetSoccerPlayerByNameAsync(input.NewName);
        }
    }
}