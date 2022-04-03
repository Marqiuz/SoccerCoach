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
    using SoccerCoach.Web.ViewModels;

    public class PositionsService : IPositionsService
    {
        private readonly IDeletableEntityRepository<Position> repository;

        public PositionsService(IDeletableEntityRepository<Position> repository)
        {
            this.repository = repository;
        }

        public async Task<PositionViewModel> GetPlayerAsync(string id)
        {
            var player = await this.repository
                .AllAsNoTracking()
                .Where(x => x.Players.First().Id == id).Select(x => new PositionViewModel
                {
                    Id = x.Id,
                    Name = x.Name.ToString(),
                    Description = x.Description,
                    Playstyle = x.Playstyle,
                    PlayerImageUrl = x.Players.First().ImageUrl,
                    Workouts = x.Workouts,
                }).FirstOrDefaultAsync();

            return player;
        }
    }
}
