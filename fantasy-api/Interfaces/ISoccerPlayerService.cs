using FantasyApi.Data.Base.Exceptions;
using FantasyApi.Data.SoccerPlayer.Dtos;
using FantasyApi.Data.SoccerPlayer.Inputs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FantasyApi.Interfaces
{
    public interface ISoccerPlayerService
    {
        Task<IEnumerable<SoccerPlayerDto>> GetPlayersAsync();
        Task<SoccerPlayerDto> GetSoccerPlayerByIdAsync(int id);
        Task<SoccerPlayerDto> GetSoccerPlayerByNameAsync(string name);
        Task<SoccerPlayerDto> GetOrAddPlayerAsync(string name);
        /// <exception cref="AlreadyExistsException"></exception>
        Task<SoccerPlayerDto> AddSoccerPlayerAsync(string name);
        /// <exception cref="NotFoundException"></exception>
        Task<SoccerPlayerDto> UpdateSoccerPlayerAsync(SoccerPlayerUpdateInput input);
    }
}
