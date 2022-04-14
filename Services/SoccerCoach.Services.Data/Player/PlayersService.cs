namespace SoccerCoach.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SoccerCoach.Data.Common.Repositories;
    using SoccerCoach.Data.Models;
    using SoccerCoach.Services.Mapping;

    public class PlayersService : IPlayersService
    {
        private readonly IDeletableEntityRepository<Player> repository;

        public PlayersService(IDeletableEntityRepository<Player> repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<T>> GetAllPlayersAsync<T>()
        {
            var players = this.repository
                .AllAsNoTracking()
                .To<T>()
                .ToList();

            return players;
        }
    }
}
