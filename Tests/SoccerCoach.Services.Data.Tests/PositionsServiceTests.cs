namespace SoccerCoach.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Moq;
    using SoccerCoach.Data.Common.Repositories;
    using SoccerCoach.Data.Models;
    using SoccerCoach.Data.Models.Enums;
    using Xunit;

    public class PositionsServiceTests
    {
        private readonly Mock<IDeletableEntityRepository<Position>> repository;

        public PositionsServiceTests()
        {
            this.repository = new Mock<IDeletableEntityRepository<Position>>();
        }

        [Fact]
        public async Task GetPlayerShouldReturnCorrectPlayer()
        {
            var positions = new List<Position>
            {
                new Position
                {
                     Name = PositionName.Striker,
                     Description = "Striker position",
                     Playstyle = "You play like a striker",
                     Players = new List<Player>
                     {
                          new Player
                          {
                           Id = "peshosId123",
                           Name = "Pesho Player",
                           TeamName = "SoftUni Coders",
                           Trophies = 6,
                           Description = "Some short description for this one",
                           Experience = "8 years",
                           Height = "1.58m",
                           Weight = "60kg",
                          },
                          new Player
                          {
                           Id = "goshosId123",
                           Name = "Gosho Player",
                           TeamName = "SoftUni Coders",
                           Trophies = 4,
                           Description = "Another short description for this one",
                           Experience = "5 years",
                           Height = "1.8m",
                           Weight = "85kg",
                          },
                     },
                },
                new Position
                {
                     Name = PositionName.Midfielder,
                     Description = "Midfielder position",
                     Playstyle = "You play like a midfielder",
                     Players = new List<Player>
                     {
                          new Player
                          {
                           Id = "toshosId123",
                           Name = "Tosho Player",
                           TeamName = "SoftUni Coders",
                           Trophies = 2,
                           Description = "Some short descr for this one",
                           Experience = "5 years",
                           Height = "1.80m",
                           Weight = "83kg",
                          },
                     },
                },
            };

            this.repository.Setup(r => r.AllAsNoTracking()).Returns(() => positions.AsQueryable());
            var service = new PositionsService(this.repository.Object);

            var position = await service.GetPlayerAsync("peshosId123");

            Assert.NotNull(service);
            Assert.Equal("Striker", position.PositionName.ToString());
        }

        [Fact]
        public async Task GetPlayerAsyncShouldReturnNullWithIdNotFound()
        {
            var positions = new List<Position>
            {
                new Position
                {
                     Name = PositionName.Striker,
                     Description = "Striker position",
                     Playstyle = "You play like a striker",
                     Players = new List<Player>
                     {
                          new Player
                          {
                           Id = "peshosId123",
                           Name = "Pesho Player",
                           TeamName = "SoftUni Coders",
                           Trophies = 6,
                           Description = "Some short description for this one",
                           Experience = "8 years",
                           Height = "1.90m",
                           Weight = "88kg",
                          },
                          new Player
                          {
                           Id = "goshosId123",
                           Name = "Gosho Player",
                           TeamName = "SoftUni Coders",
                           Trophies = 4,
                           Description = "Another short description for this one",
                           Experience = "5 years",
                           Height = "1.8m",
                           Weight = "85kg",
                          },
                     },
                },
                new Position
                {
                     Name = PositionName.Midfielder,
                     Description = "Midfielder position",
                     Playstyle = "You play like a Midfielder",
                     Players = new List<Player>
                     {
                          new Player
                          {
                           Id = "toshosId123",
                           Name = "Tosho Player",
                           TeamName = "SoftUni Coders",
                           Trophies = 2,
                           Description = "Some short descr for this one",
                           Experience = "5 years",
                           Height = "1.67m",
                           Weight = "75kg",
                          },
                     },
                },
            };

            this.repository.Setup(r => r.AllAsNoTracking()).Returns(() => positions.AsQueryable());
            var service = new PositionsService(this.repository.Object);

            var position = await service.GetPlayerAsync("peshoNotFound123");

            Assert.Null(position);
        }

        [Fact]
        public void GetPositionByIdShouldReturnTheRightPosition()
        {
            var positions = new List<Position>
            {
                new Position
                {
                     Id = "position1",
                     Name = PositionName.Striker,
                     Description = "Striker position",
                     Playstyle = "You play like a Striker",
                     Players = new List<Player>
                     {
                          new Player
                          {
                           Id = "peshosId123",
                           Name = "Pesho Player",
                           TeamName = "SoftUni Coders",
                           Trophies = 6,
                           Description = "Some short description for this one",
                           Experience = "8 years",
                           Height = "1.85m",
                           Weight = "86kg",
                          },
                          new Player
                          {
                           Id = "goshosId123",
                           Name = "Gosho Player",
                           TeamName = "SoftUni Coders",
                           Trophies = 4,
                           Description = "Another short description for this one",
                           Experience = "5 years",
                           Height = "1.8m",
                           Weight = "81kg",
                          },
                     },
                },
                new Position
                {
                     Id = "position2",
                     Name = PositionName.Midfielder,
                     Description = "Midfielder position",
                     Playstyle = "You play like a Midfielder",
                     Players = new List<Player>
                     {
                          new Player
                          {
                           Id = "toshosId123",
                           Name = "Toshoo Player",
                           TeamName = "SoftUni Coders",
                           Trophies = 2,
                           Description = "Some short descr for this one",
                           Experience = "5 years",
                           Height = "1.70m",
                           Weight = "61kg",
                          },
                     },
                },
            };

            this.repository.Setup(r => r.AllAsNoTracking()).Returns(() => positions.AsQueryable());
            var service = new PositionsService(this.repository.Object);

            var position = service.GetPositionById("position1");

            Assert.Equal("Striker", position.Name.ToString());
            Assert.Equal(2, position.Players.Count());
        }

        [Fact]
        public void GetPositionByNameShouldReturnTheRightPosition()
        {
            var positions = new List<Position>
            {
                new Position
                {
                     Id = "position1",
                     Name = PositionName.Striker,
                     Description = "Striker position",
                     Playstyle = "You play like a Striker",
                     Players = new List<Player>
                     {
                          new Player
                          {
                           Id = "peshosId123",
                           Name = "Pesho Player",
                           TeamName = "SoftUni Coders",
                           Trophies = 6,
                           Description = "Some short description for this one",
                           Experience = "8 years",
                           Height = "1.65m",
                           Weight = "74kg",
                          },
                          new Player
                          {
                           Id = "goshosId123",
                           Name = "Gosho Player",
                           TeamName = "SoftUni Coders",
                           Trophies = 4,
                           Description = "Another short description for this one",
                           Experience = "5 years",
                           Height = "1.8m",
                           Weight = "85kg",
                          },
                     },
                },
                new Position
                {
                     Id = "position2",
                     Name = PositionName.Midfielder,
                     Description = "Midfielder position",
                     Playstyle = "You play like a Midfielder",
                     Players = new List<Player>
                     {
                          new Player
                          {
                           Id = "toshosId123",
                           Name = "Tosho Player",
                           TeamName = "SoftUni Coders",
                           Trophies = 2,
                           Description = "Some short descr for this one",
                           Experience = "5 years",
                           Height = "1.90m",
                           Weight = "91kg",
                          },
                     },
                },
            };

            this.repository.Setup(r => r.AllAsNoTracking()).Returns(() => positions.AsQueryable());
            var service = new PositionsService(this.repository.Object);

            var name = PositionName.Striker;
            var position = service.GetPositionByName(name);

            Assert.Equal("Striker", position.Name.ToString());
            Assert.NotNull(position);
            this.repository.Verify(x => x.AllAsNoTracking(), Times.Once);
        }
    }
}
