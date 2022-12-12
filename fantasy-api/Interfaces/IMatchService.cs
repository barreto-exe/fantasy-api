using FantasyApi.Data.Base.Dtos;
using FantasyApi.Data.Base.Requests;
using FantasyApi.Data.Matches.Dtos;
using FantasyApi.Data.Matches.Inputs;
using System.Threading.Tasks;

namespace FantasyApi.Interfaces
{
    public interface IMatchService
    {
        Task<MatchDto> GetMatchByIdAsync(int id);
        Task<PaginatedListDto<MatchDto>> GetMatchesPaginatedAsync(BaseRequest filter);
        Task<MatchDto> AddMatchAsync(MatchAddInput input);
        Task<MatchDto> UpdateMatchAsync(MatchUpdateInput input);
        Task DeleteMatchAsync(int id);
    }
}
