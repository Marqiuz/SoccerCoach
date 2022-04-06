namespace SoccerCoach.Services.Data.Workout
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using SoccerCoach.Data.Common.Repositories;
    using SoccerCoach.Data.Models;
    using SoccerCoach.Services.Mapping;
    using SoccerCoach.Web.ViewModels.Workouts;

    public class WorkoutsService : IWorkoutsService
    {
        private readonly IDeletableEntityRepository<Workout> workoutsRepository;
        private readonly IDeletableEntityRepository<Position> positionsRepository;
        private readonly IDeletableEntityRepository<Coach> coachesRepository;

        public WorkoutsService(
            IDeletableEntityRepository<Workout> workoutsRepository,
            IDeletableEntityRepository<Position> positionsRepository,
            IDeletableEntityRepository<Coach> coachesRepository)
        {
            this.workoutsRepository = workoutsRepository;
            this.positionsRepository = positionsRepository;
            this.coachesRepository = coachesRepository;
        }

        public async Task CreateWorkoutAsync(CreateWorkoutInputModel input, string userId)
        {
            var position = this.positionsRepository.AllAsNoTracking().First(x => x.Name == input.PositionName);
            var coach = this.coachesRepository.AllAsNoTracking().FirstOrDefault(x => x.UserId == userId);

            var workout = new Workout
            {
                Name = input.Name,
                Description = input.Description,
                VideoUrl = input.VideoUrl,
                PositionId = position.Id,
                CoachId = coach.Id,
            };

            await this.workoutsRepository.AddAsync(workout);
            await this.workoutsRepository.SaveChangesAsync();
        }

        public ICollection<T> GetAll<T>()
        {
            return this.workoutsRepository
                .AllAsNoTracking()
                .OrderByDescending(x => x.CreatedOn)
                .To<T>()
                .ToList();
        }
    }
}
