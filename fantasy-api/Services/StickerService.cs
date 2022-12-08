﻿using FantasyApi.Data.Base.Exceptions;
using FantasyApi.Data.Stickers.Dto;
using FantasyApi.Data.Stickers.Inputs;
using FantasyApi.Interfaces;
using MySqlConnector;
using System;
using System.Collections.Generic;
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
            return await GetItemByIdAsync<StickerDto>("GetStickerById", "s_id", id);
        }

        public async Task<IEnumerable<StickerDto>> GetStickersAsync()
        {
            return await GetItemsAsync<StickerDto>("GetStickers");
        }

        public async Task<StickerDto> AddStickerAsync(StickerAddInput input)
        {
            var sPlayer = await _playerService.GetSoccerPlayerByNameAsync(input.PlayerName);
            if (sPlayer == null) throw new NotFoundException("Player with requested id.");

            var sEvent = await _eventService.GetEventByIdAsync(input.EventId);
            if (sEvent == null) throw new NotFoundException("Event with requested id.");

            var sTeam = await _teamService.GetTeamByIdAsync(input.TeamId);
            if (sTeam == null) throw new NotFoundException("Team with requested id.");

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

            var sPlayer = await _playerService.GetSoccerPlayerByNameAsync(input.PlayerName ?? old.PlayerName);
            var sEvent = await _eventService.GetEventByIdAsync(input.EventId ?? old.EventId);
            var sTeam = await _teamService.GetTeamByIdAsync(input.TeamId ?? old.TeamId);


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
