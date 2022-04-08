namespace SoccerCoach.Services.Data.Workout
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SoccerCoach.Data.Models;
    using SoccerCoach.Web.ViewModels.Workouts;

    public interface IWorkoutsService
    {
        (IEnumerable<T> Workouts, int Count) GetSearchedPositions<T>(SearchWorkoutInputModel inputModel, int page, int itemsPerPage);

        IEnumerable<T> GetAll<T>(int page, int itemsPerPage);

        int GetCount();

        Task CreateAsync(CreateWorkoutInputModel input, string userId);

        EditWorkoutViewModel GetWorkoutForEdit(string id);

        Task EditAsync(EditWorkoutViewModel input);

        Task DeleteAsync(string id);

        Workout GetWorkoutById(string id);
    }
}
