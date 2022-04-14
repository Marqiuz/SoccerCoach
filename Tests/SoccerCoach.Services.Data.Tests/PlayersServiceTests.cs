namespace SoccerCoach.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    using Moq;
    using SoccerCoach.Data.Common.Repositories;
    using SoccerCoach.Data.Models;
    using SoccerCoach.Data.Models.Enums;
    using SoccerCoach.Services.Mapping;
    using SoccerCoach.Web.Controllers;
    using Xunit;

    public class PlayersServiceTests
    {
        private readonly Mock<IDeletableEntityRepository<Player>> repository;

        public PlayersServiceTests()
        {
            this.repository = new Mock<IDeletableEntityRepository<Player>>();
            AutoMapperConfig.RegisterMappings(Assembly.Load("SoccerCoach.Web.ViewModels"));
        }

        [Fact]
        public void GetAllPlayersShouldReturnCollectionOfPlayers()
        {
            var players = new List<Player>
            {
                    new Player
                          {
                           Id = "peshosId123",
                           Position = new Position
                           {
                                Name = PositionName.Midfielder,
                                Description = "Midfielder position",
                                Playstyle = "You play like a midfielder",
                           },
                           Name = "Pesho Player",
                           TeamName = "SoftUni Coders",
                           Trophies = 6,
                           Description = "Some short description for this one",
                           Experience = "9 years",
                           Height = "1.85m",
                           Weight = "78kg",
                          },
                    new Player
                          {
                           Id = "goshosId123",
                           Position = new Position
                           {
                                Name = PositionName.Striker,
                                Description = "Striker position",
                                Playstyle = "You play like a Striker",
                           },
                           Name = "Gosho Player",
                           TeamName = "SoftUni Coders",
                           Trophies = 4,
                           Description = "Another short description for this one",
                           Experience = "2 years",
                           Height = "1.74m",
                           Weight = "83kg",
                          },
                    new Player
                          {
                           Id = "toshosId123",
                           Position = new Position
                           {
                                Name = PositionName.Goalkeeper,
                                Description = "Goalkeeper position",
                                Playstyle = "You play like a Goalkeeper",
                           },
                           Name = "Tosho Player",
                           TeamName = "SoftUni Coders",
                           Trophies = 2,
                           Description = "Some short description for this one",
                           Experience = "5 years",
                           Height = "1.90m",
                           Weight = "91kg",
                          },
            };

            this.repository.Setup(r => r.AllAsNoTracking()).Returns(() => players.AsQueryable());
            var service = new PlayersService(this.repository.Object);

            var playersCollection = service.GetAllPlayersAsync<PlayerViewModel>();

            Assert.Equal(3, playersCollection.Result.Count());
        }
    }
}
