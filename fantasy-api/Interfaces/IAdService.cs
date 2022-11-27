using FantasyApi.Data.Ads.Dtos;
using FantasyApi.Data.Ads.Inputs;
using FantasyApi.Data.Base.Dtos;
using FantasyApi.Data.Base.Requests;
using System.Threading.Tasks;

namespace FantasyApi.Interfaces
{
    public interface IAdService
    {
        Task<AdDto> GetAdByIdAsync(int id);
        Task<PaginatedListDto<AdDto>> GetAdsPaginatedAsync(BaseRequest filter);
        Task<AdDto> AddAdAsync(AdAddInput input);
        Task<AdDto> UpdateAdAsync(AdUpdateInput input);
        Task DeleteAdAsync(int id);

        Task<AdForUserDto> GetAdByRandomRequestAsync();
        Task AddAdClickAsync(int id);
    }
}
