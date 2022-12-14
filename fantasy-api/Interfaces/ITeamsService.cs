using FantasyApi.Data.Base.Dtos;
using FantasyApi.Data.Base.Exceptions;
using FantasyApi.Data.Teams.Dtos;
using FantasyApi.Data.Teams.Filters;
using FantasyApi.Data.Teams.Inputs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FantasyApi.Interfaces
{
    public interface ITeamsService
    {
        Task<TeamDto> GetTeamByIdAsync(int id);

        Task<IEnumerable<TeamDto>> GetTeamsAsync();

        Task<IEnumerable<TeamDto>> GetTeamsAsync(TeamsFilter filter);

        Task<PaginatedListDto<TeamDto>> GetTeamsPaginatedAsync(TeamsFilter filter);

        /// <exception cref="NotFoundException"></exception>
        Task<TeamDto> AddTeamAsync(TeamAddInput input);

        /// <exception cref="NotFoundException"></exception>
        Task<TeamDto> UpdateTeamAsync(TeamUpdateInput input);

        /// <exception cref="NotFoundException"></exception>
        Task DeleteTeamAsync(int id);
    }
}
