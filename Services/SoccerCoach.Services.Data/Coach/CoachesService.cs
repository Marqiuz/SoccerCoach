namespace SoccerCoach.Services.Data.Coach
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SoccerCoach.Common;
    using SoccerCoach.Data.Common.Repositories;
    using SoccerCoach.Data.Models;
    using SoccerCoach.Services.Data.Picture;
    using SoccerCoach.Services.Mapping;
    using SoccerCoach.Web.ViewModels;
    using SoccerCoach.Web.ViewModels.Coaches;

    public class CoachesService : ICoachesService
    {
        private readonly IDeletableEntityRepository<Coach> coachRepository;

        public CoachesService(
            IDeletableEntityRepository<Coach> coachRepository)
        {
            this.coachRepository = coachRepository;
        }

        public async Task<bool> CreateCoachAsync(CreateCoachInputModel input, ApplicationUser user)
        {
            var coach = new Coach
            {
                Name = input.FullName,
                Email = input.Email,
                Description = input.Description,
                Phone = input.Phone,
                Experience = input.Experience,
                User = user,
                UserId = user.Id,
            };

            if (coach != null && coach.Experience > 0)
            {
                await this.coachRepository.AddAsync(coach);
                await this.coachRepository.SaveChangesAsync();

                return true;
            }

            throw new InvalidOperationException(GlobalConstants.InvalidOperationExceptionWhileCreatingCoach);
        }

        public async Task<IEnumerable<T>> GetAllCoachesAsync<T>()
        {
            var coaches = await this.coachRepository
                .AllAsNoTracking()
                .To<T>()
                .ToListAsync();

            return coaches;
        }

        public async Task<CoachViewModel> GetCoachById(string id)
        {
            var coach = await this.coachRepository
                .AllAsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new CoachViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Experience = x.Experience,
                    Phone = x.Phone,
                    Email = x.Email,
                    CoachWorkoutsCount = x.CoachWorkouts.Count(),
                    CoursesCount = x.Courses.Count(),
                    Description = x.Description,
                    AverageVote = !x.Votes.Any() ? 0 : x.Votes.Average(x => x.Value),
                }).FirstOrDefaultAsync();

            return coach;
        }

        public Coach GetCoachByUserId(string id)
        {
            var coach = this.coachRepository.AllAsNoTracking().FirstOrDefault(x => x.UserId == id);

            return coach;
        }
    }
}
