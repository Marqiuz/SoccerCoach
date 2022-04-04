namespace SoccerCoach.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SoccerCoach.Data.Common.Repositories;
    using SoccerCoach.Data.Models;
    using SoccerCoach.Web.Controllers;

    public class PlayersService : IPlayersService
    {
        private readonly IDeletableEntityRepository<Player> repository;

        public PlayersService(IDeletableEntityRepository<Player> repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<PlayerViewModel>> GetAllPlayersAsync()
        {
            var players = await this.repository
                .AllAsNoTracking()
                .Select(x => new PlayerViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    PositionName = x.Position.Name,
                    TeamName = x.TeamName,
                    Trophies = x.Trophies,
                    Height = x.Height,
                    Weight = x.Weight,
                    Experience = x.Experience,
                    ImageUrl = x.ImageUrl,
                }).ToListAsync();

            return players;
        }
    }
}
