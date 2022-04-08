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
    using SoccerCoach.Services.Data.Coach;
    using SoccerCoach.Services.Mapping;
    using SoccerCoach.Web.ViewModels.Workouts;

    public class WorkoutsService : IWorkoutsService
    {
        private readonly IDeletableEntityRepository<Workout> workoutsRepository;
        private readonly IPositionsService positionsService;
        private readonly ICoachesService coachesService;

        public WorkoutsService(
            IDeletableEntityRepository<Workout> workoutsRepository,
            IPositionsService positionsService,
            ICoachesService coachesService)
        {
            this.workoutsRepository = workoutsRepository;
            this.positionsService = positionsService;
            this.coachesService = coachesService;
        }

        public async Task CreateAsync(CreateWorkoutInputModel input, string userId)
        {
            var position = this.positionsService.GetPositionByName(input.PositionName);
            var coach = this.coachesService.GetCoachByUserId(userId);

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
            var position = this.positionsService.GetPositionByName(input.PositionName);

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
            var position = this.positionsService.GetPositionById(workout.PositionId);

            if (workout != null)
            {
                var model = new EditWorkoutViewModel()
                {
                    Id = workout.Id,
                    Name = workout.Name,
                    PositionName = position.Name,
                    Description = workout.Description,
                    VideoUrl = workout.VideoUrl,
                    CoachId = workout.CoachId,
                };

                return model;
            }

            throw new InvalidOperationException(GlobalConstants.InvalidOperationExceptionInWorkoutForEditSearch);
        }

        public async Task DeleteAsync(string id)
        {
            var workout = this.GetWorkoutById(id);
            this.workoutsRepository.Delete(workout);
            await this.workoutsRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>(int page, int itemsPerPage)
        {
            return this.workoutsRepository
                .AllAsNoTracking()
                .OrderByDescending(x => x.CreatedOn)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .To<T>()
                .ToList();
        }

        public Workout GetWorkoutById(string id)
        {
            return this.workoutsRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public int GetCount()
        {
            return this.workoutsRepository.AllAsNoTracking().Count();
        }
    }
}
