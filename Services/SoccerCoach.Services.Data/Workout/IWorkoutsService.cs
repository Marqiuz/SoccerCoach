namespace SoccerCoach.Services.Data.Workout
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SoccerCoach.Web.ViewModels.Workouts;

    public interface IWorkoutsService
    {
        ICollection<T> GetAll<T>();

        Task CreateWorkoutAsync(CreateWorkoutInputModel input, string userId);
    }
}