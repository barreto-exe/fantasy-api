using FantasyApi.Data.Base.Exceptions;
using FantasyApi.Data.Stickers.Dto;
using FantasyApi.Data.Stickers.Inputs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FantasyApi.Interfaces
{
    public interface IStickerService
    {
        Task<StickerDto> GetStickerByIdAsync(int id);

        Task<IEnumerable<StickerDto>> GetStickersAsync();

        /// <exception cref="AlreadyExistsException"></exception>
        Task<StickerDto> AddStickerAsync(StickerAddInput input);

        /// <exception cref="NotFoundException"></exception>
        Task<StickerDto> UpdateStickerAsync(StickerUpdateInput input);

        /// <exception cref="NotFoundException"></exception>
        Task DeleteStickerAsync(int id);
    }
}
