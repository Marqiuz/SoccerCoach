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
    using SoccerCoach.Services.Data.Coach;
    using SoccerCoach.Services.Mapping;
    using SoccerCoach.Web.ViewModels.Coaches;
    using Xunit;

    public class CoachesServiceTests
    {
        private readonly Mock<IDeletableEntityRepository<Coach>> coachRepository;
        private readonly CoachesService coachesService;

        public CoachesServiceTests()
        {
            this.coachRepository = new Mock<IDeletableEntityRepository<Coach>>();
            this.coachesService = new CoachesService(this.coachRepository.Object);
        }

        [Fact]
        public void GetCoachByUserIdShouldWorkWhenCoachExists()
        {
            var coaches = new List<Coach>
            {
                new Coach
                           {
                               Id = "c1",
                               Name = "Coach1",
                               Description = "desc1",
                               Experience = 2,
                               Phone = "321312312",
                               Email = "coach123@abv.bg",
                               User = new ApplicationUser { Id = "coachuser" },
                               UserId = "coachuser",
                           },
                new Coach
                           {
                               Id = "c2",
                               Name = "Coach2",
                               Description = "desc2",
                               Experience = 5,
                               Phone = "32133213212312",
                               Email = "coach1234@abv.bg",
                               User = new ApplicationUser { Id = "coachuser2" },
                               UserId = "coachuser2",
                           },
            };

            this.coachRepository.Setup(r => r.AllAsNoTracking()).Returns(() => coaches.AsQueryable());

            var coach = this.coachesService.GetCoachByUserId("coachuser2");

            Assert.Equal("c2", coach.Id);
            Assert.Equal(5, coach.Experience);
            this.coachRepository.Verify(x => x.AllAsNoTracking(), Times.Once);
        }

        [Fact]
        public async Task GetCoachByIdShouldReturnCoachViewModel()
        {
            var coaches = new List<Coach>
            {
                new Coach
                           {
                               Id = "c1",
                               Name = "Coach1",
                               Description = "desc1",
                               Experience = 2,
                               Phone = "321312312",
                               Email = "coach123@abv.bg",
                               User = new ApplicationUser { Id = "coachuser" },
                               UserId = "coachuser",
                           },
                new Coach
                           {
                               Id = "c2",
                               Name = "Coach2",
                               Description = "desc2",
                               Experience = 5,
                               Phone = "32133213212312",
                               Email = "coach1234@abv.bg",
                               User = new ApplicationUser { Id = "coachuser2" },
                               UserId = "coachuser2",
                           },
            };

            this.coachRepository.Setup(r => r.AllAsNoTracking()).Returns(() => coaches.AsQueryable());

            var coach = await this.coachesService.GetCoachById("c1");

            Assert.Equal("c1", coach.Id);
            Assert.Equal(2, coach.Experience);
            Assert.Equal(0, coach.CoachWorkoutsCount);
            this.coachRepository.Verify(x => x.AllAsNoTracking(), Times.Once);
        }
    }
}
