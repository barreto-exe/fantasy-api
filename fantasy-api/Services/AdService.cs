using FantasyApi.Data.Ads.Dtos;
using FantasyApi.Data.Ads.Inputs;
using FantasyApi.Data.Base.Dtos;
using FantasyApi.Data.Base.Exceptions;
using FantasyApi.Data.Base.Requests;
using FantasyApi.Interfaces;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FantasyApi.Services
{
    public class AdService : BaseService, IAdService
    {
        private readonly IAzureStorageService _storageService;
        public AdService(IDatabaseService databaseService, IAzureStorageService storageService) : base(databaseService)
        {
            _storageService = storageService;
        }

        public async Task<AdDto> GetAdByIdAsync(int id)
        {
            return await GetItemByIdAsync<AdDto>("GetAdById", "ad_id", id);
        }

        public async Task<PaginatedListDto<AdDto>> GetAdsPaginatedAsync(BaseRequest filter)
        {
            return await GetItemsPaginatedAsync<AdDto>(filter, "GetAdsPaginated");
        }

        public async Task<AdDto> AddAdAsync(AdAddInput input)
        {
            List<MySqlParameter> parameters = new()
            {
                new MySqlParameter("new_alias", input.Alias),
                new MySqlParameter("new_description", input.Description),
                new MySqlParameter("new_type", input.AdType),
                new MySqlParameter("new_redirect", input.RedirectTo),
            };

            if (input.Img != null)
            {
                var blob = await _storageService.UploadAsync(input.Img);
                parameters.Add(new MySqlParameter("new_img_url", blob.Blob.Uri));
            }
            else
            {
                parameters.Add(new MySqlParameter("new_img_url", ""));
            }

            var cmd = _databaseService.GetCommand("AddAd", parameters);
            var data = await _databaseService.ExecuteStoredProcedureAsync(cmd);
            if (data.Rows.Count > 0)
            {
                int newId = Convert.ToInt32(data.Rows[0][0]);
                return await GetAdByIdAsync(newId);
            }
            else
            {
                return null;
            }
        }

        public async Task<AdDto> UpdateAdAsync(AdUpdateInput input)
        {
            var old = await GetAdByIdAsync(input.Id);
            if (old == null)
            {
                throw new NotFoundException("Ad with the requested id");
            }

            List<MySqlParameter> parameters = new()
            {
                new MySqlParameter("ad_id", input.Id),
                new MySqlParameter("new_alias", input.Alias ?? old.Alias),
                new MySqlParameter("new_description", input.Description ?? old.Description),
                new MySqlParameter("new_type", input.AdType ?? old.AdType),
                new MySqlParameter("new_redirect", input.RedirectTo ?? old.RedirectTo),
            };

            if (input.Img != null)
            {
                var blob = await _storageService.UploadAsync(input.Img);
                parameters.Add(new MySqlParameter("new_img_url", blob.Blob.Uri));
            }
            else
            {
                parameters.Add(new MySqlParameter("new_img_url", old.Img));
            }

            var cmd = _databaseService.GetCommand("UpdateAd", parameters);
            await _databaseService.ExecuteStoredProcedureAsync(cmd);

            return await GetAdByIdAsync(input.Id);
        }

        public async Task DeleteAdAsync(int id)
        {
            var old = await GetAdByIdAsync(id);
            if (old == null)
            {
                throw new NotFoundException("Ad with the requested id");
            }

            await DeleteItemAsync("DeleteAd", "ad_id", id);
        }

    }
}
