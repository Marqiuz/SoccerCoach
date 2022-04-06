namespace SoccerCoach.Services.Data.Workout
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SoccerCoach.Data.Models;
    using SoccerCoach.Web.ViewModels.Workouts;

    public interface IWorkoutsService
    {
        ICollection<T> GetAll<T>();

        Task CreateAsync(CreateWorkoutInputModel input, string userId);

        EditWorkoutViewModel GetWorkoutForEdit(string id);

        Task EditAsync(EditWorkoutViewModel input);

        Task DeleteAsync(string id);

        Workout GetWorkoutById(string id);
    }
}
