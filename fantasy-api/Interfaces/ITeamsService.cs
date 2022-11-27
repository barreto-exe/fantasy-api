﻿using FantasyApi.Data.Base.Dtos;
using FantasyApi.Data.Teams.Dtos;
using FantasyApi.Data.Teams.Filters;
using FantasyApi.Data.Teams.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyApi.Interfaces
{
    public interface ITeamsService
    {
        Task<TeamDto> GetTeamByIdAsync(int id);

        Task<IEnumerable<TeamDto>> GetTeamsAsync(TeamsFilter filter);

        /// <exception cref="NotFoundException"></exception>
        Task<TeamDto> AddTeamAsync(TeamAddInput input);
    }
}
