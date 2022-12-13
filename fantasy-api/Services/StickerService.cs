using Core.Utils.Mapping;
using FantasyApi.Data.Base.Dtos;
using FantasyApi.Data.Base.Exceptions;
using FantasyApi.Data.SoccerPlayer.Inputs;
using FantasyApi.Data.Stickers.Dto;
using FantasyApi.Data.Stickers.Filters;
using FantasyApi.Data.Stickers.Inputs;
using FantasyApi.Interfaces;
using FantasyApi.Utils;
using Microsoft.Azure.Amqp;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace FantasyApi.Services
{
    public class StickerService : BaseService, IStickerService
    {
        private readonly IAzureStorageService _storageService;
        private readonly ISoccerPlayerService _playerService;
        private readonly IEventService _eventService;
        private readonly ITeamsService _teamService;

        public StickerService(IDatabaseService databaseService,
            IAzureStorageService storageService,
            ISoccerPlayerService soccerPlayerService,
            IEventService eventService,
            ITeamsService teamsService) : base(databaseService)
        {
            _storageService = storageService;
            _playerService = soccerPlayerService;
            _eventService = eventService;
            _teamService = teamsService;
        }

        public async Task<StickerDto> GetStickerByIdAsync(int id)
        {
            StickerDto sticker = await GetItemByIdAsync<StickerDto>("GetStickerById", "s_id", id);
            if (sticker == null) return null;

            sticker.Event =
                (await _eventService.GetEventsAsync())
                .FirstOrDefault(x => x.Id == sticker.EventId);

            sticker.Team =
                (await _teamService.GetTeamsAsync())
                .FirstOrDefault(x => x.Id == sticker.TeamId);

            return sticker;
        }

        public async Task<IEnumerable<StickerDto>> GetStickersAsync()
        {
            var stickers = (await GetItemsAsync<StickerDto>("GetStickers")).ToList();

            var events = await _eventService.GetEventsAsync();
            var teams = await _teamService.GetTeamsAsync();

            stickers.ForEach(s =>
            {
                s.Event = events.FirstOrDefault(x => x.Id == s.EventId);
                s.Team = teams.FirstOrDefault(x => x.Id == s.TeamId);
            });

            return stickers;
        }

        public async Task<PaginatedListDto<StickerDto>> GetStickersAsync(StickersFilter filter)
        {
            List<MySqlParameter> parameters = new()
            {
                new MySqlParameter("s_event_id", filter.EventId ?? 0),
                new MySqlParameter("s_team_id", filter.TeamId ?? 0),
                new MySqlParameter("page", filter.Page),
                new MySqlParameter("size", filter.Size),
            };

            var cmd = _databaseService.GetCommand("GetStickersFiltered", parameters);
            var data = await _databaseService.ExecuteStoredProcedureDataSetAsync(cmd);

            if (data.Tables[0].Rows.Count > 0)
            {
                var mapper = new DataNamesMapper<StickerDto>();
                var stickers = mapper.Map(data.Tables[0]).ToList();

                var events = await _eventService.GetEventsAsync();
                var teams = await _teamService.GetTeamsAsync();

                stickers.ForEach(s =>
                {
                    s.Event = events.FirstOrDefault(x => x.Id == s.EventId);
                    s.Team = teams.FirstOrDefault(x => x.Id == s.TeamId);
                });

                int totalRows = (int)data.Tables[1].Rows[0]["totalRows"];
                int page = (int)data.Tables[1].Rows[0]["page"];
                int totalPages = (int)data.Tables[1].Rows[0]["totalPages"];
                int size = (int)data.Tables[1].Rows[0]["size"];

                return ResponseBuilder.PaginatedListResponse(stickers, totalRows, page, totalPages, size);
            }
            else
            {
                return null;
            }
        }

        public async Task<StickerDto> AddStickerAsync(StickerAddInput input)
        {
            var sPlayer = await _playerService.GetOrAddPlayerAsync(new SoccerPlayerAddInput
            {
                Name = input.PlayerName,
                ExternalUuid = input.ExternalUuid,
            });

            if(input.ExternalUuid != sPlayer.ExternalUuid)
            {
                throw new Exception("External UUID doesn't coincide with the existing one.");
            }

            var sTeam = await _teamService.GetTeamByIdAsync(input.TeamId);
            if (sTeam == null) throw new NotFoundException("Team with requested id.");
            if (!sTeam.EventIds.Contains(input.EventId)) throw new NotFoundException("Team on requested event");

            List<MySqlParameter> parameters = new()
            {
                new MySqlParameter("s_id_player", sPlayer.Id),
                new MySqlParameter("s_id_event", input.EventId),
                new MySqlParameter("s_id_team", input.TeamId),
                new MySqlParameter("s_height", input.Height),
                new MySqlParameter("s_weight", input.Weight),
                new MySqlParameter("s_position", input.Position),
                new MySqlParameter("s_appearance_rate", input.AppearanceRate),
            };

            if (input.Img != null)
            {
                var blob = await _storageService.UploadAsync(input.Img);
                parameters.Add(new MySqlParameter("img_sticker", blob.Blob.Uri));
            }
            else
            {
                parameters.Add(new MySqlParameter("img_sticker", ""));
            }

            var cmd = _databaseService.GetCommand("AddSticker", parameters);
            var data = await _databaseService.ExecuteStoredProcedureAsync(cmd);
            if (data.Rows.Count > 0)
            {
                int newId = Convert.ToInt32(data.Rows[0][0]);
                return await GetStickerByIdAsync(newId);
            }
            else
            {
                return null;
            }
        }

        public async Task<StickerDto> UpdateStickerAsync(StickerUpdateInput input)
        {
            var old = await GetStickerByIdAsync(input.Id);
            if (old == null)
            {
                throw new NotFoundException("Sticker with the requested id");
            }

            var oldPlayer = await _playerService.GetSoccerPlayerByNameAsync(old.PlayerName);
            var sPlayer = await _playerService.UpdateSoccerPlayerAsync(new SoccerPlayerUpdateInput
            {
                Id = oldPlayer.Id,
                Name = input.PlayerName ?? old.PlayerName,
                ExternalUuid = input.ExternalUuid ?? old.ExternalUuid,
            });
            var sEvent = await _eventService.GetEventByIdAsync(input.EventId ?? old.EventId);
            var sTeam = await _teamService.GetTeamByIdAsync(input.TeamId ?? old.TeamId);

            //Check if team and event exists
            if (sTeam == null) throw new NotFoundException("Team with requested id.");
            if (input.EventId != null && !sTeam.EventIds.Contains((int)input.EventId))
            {
                throw new NotFoundException("Team on requested event");
            }

            List<MySqlParameter> parameters = new()
            {
                new MySqlParameter("sticker_id", input.Id),
                new MySqlParameter("s_id_player", sPlayer.Id),
                new MySqlParameter("s_id_event", sEvent.Id),
                new MySqlParameter("s_id_team", sTeam.Id),
                new MySqlParameter("s_height", input.Height ?? old.Height),
                new MySqlParameter("s_weight", input.Weight ?? old.Weight),
                new MySqlParameter("s_position", input.Position ?? old.Position),
                new MySqlParameter("s_appearance_rate", input.AppearanceRate ?? old.AppearanceRate),
            };

            if (input.Img != null)
            {
                var blob = await _storageService.UploadAsync(input.Img);
                parameters.Add(new MySqlParameter("img_sticker", blob.Blob.Uri));
            }
            else
            {
                parameters.Add(new MySqlParameter("img_sticker", old.Img));
            }

            var cmd = _databaseService.GetCommand("UpdateSticker", parameters);
            await _databaseService.ExecuteStoredProcedureAsync(cmd);

            return await GetStickerByIdAsync(input.Id);
        }

        public async Task DeleteStickerAsync(int id)
        {
            var old = await GetStickerByIdAsync(id);
            if (old == null)
            {
                throw new NotFoundException("Sticker with the requested id");
            }

            await DeleteItemAsync("DeleteSticker", "idSticker", id);
        }
    }
}
