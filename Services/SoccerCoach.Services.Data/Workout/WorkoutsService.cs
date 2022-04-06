namespace SoccerCoach.Services.Data.Workout
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using SoccerCoach.Common;
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

        public async Task CreateAsync(CreateWorkoutInputModel input, string userId)
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

        public async Task EditAsync(EditWorkoutViewModel input)
        {
            var workout = this.GetWorkoutById(input.Id);
            var position = this.positionsRepository.AllAsNoTracking().First(x => x.Name == input.PositionName);

            workout.Name = input.Name;
            workout.PositionId = position.Id;
            workout.Description = input.Description;
            workout.VideoUrl = input.VideoUrl;

            this.workoutsRepository.Update(workout);
            await this.workoutsRepository.SaveChangesAsync();
        }

        public EditWorkoutViewModel GetWorkoutForEdit(string id)
        {
            var workout = this.GetWorkoutById(id);
            var position = this.positionsRepository.AllAsNoTracking().First(x => x.Id == workout.PositionId);

            if (workout != null)
            {
                var model = new EditWorkoutViewModel()
                {
                    Id = workout.Id,
                    Name = workout.Name,
                    PositionName = position.Name,
                    Description = workout.Description,
                    VideoUrl = workout.VideoUrl,
                };

                return model;
            }

            throw new InvalidOperationException(GlobalConstants.InvalidOperationExceptionInWorkoutForEditSearch);
        }

        public ICollection<T> GetAll<T>() => this.workoutsRepository
                .AllAsNoTracking()
                .OrderByDescending(x => x.CreatedOn)
                .To<T>()
                .ToList();

        public Workout GetWorkoutById(string id) => this.workoutsRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == id);

        public async Task DeleteAsync(string id)
        {
            var workout = this.GetWorkoutById(id);
            this.workoutsRepository.Delete(workout);
            await this.workoutsRepository.SaveChangesAsync();
        }
    }
}
