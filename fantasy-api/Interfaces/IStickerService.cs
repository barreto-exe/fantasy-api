using FantasyApi.Data.Base.Dtos;
using FantasyApi.Data.Base.Exceptions;
using FantasyApi.Data.Stickers.Dto;
using FantasyApi.Data.Stickers.Filters;
using FantasyApi.Data.Stickers.Inputs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FantasyApi.Interfaces
{
    public interface IStickerService
    {
        Task<StickerDto> GetStickerByIdAsync(int id);

        Task<IEnumerable<StickerDto>> GetStickersAsync();
        Task<PaginatedListDto<StickerDto>> GetStickersAsync(StickersFilter filter);

        /// <exception cref="NotFoundException"></exception>
        Task<StickerDto> AddStickerAsync(StickerAddInput input);

        /// <exception cref="NotFoundException"></exception>
        Task<StickerDto> UpdateStickerAsync(StickerUpdateInput input);

        /// <exception cref="NotFoundException"></exception>
        Task DeleteStickerAsync(int id);
    }
}
