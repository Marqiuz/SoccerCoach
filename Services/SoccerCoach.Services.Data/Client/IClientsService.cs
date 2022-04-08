namespace SoccerCoach.Services.Data.Client
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using SoccerCoach.Data.Models;
    using SoccerCoach.Services.Data.Models;
    using SoccerCoach.Web.ViewModels;
    using SoccerCoach.Web.ViewModels.Workouts;

    public interface IClientsService
    {
        int GetCount(string userId);

        ClientDto GetClient(string userId);

        Task<IEnumerable<WorkoutInListViewModel>> GetWorkouts(string userId, int page, int itemsPerPage);

        Task<bool> CreateClientAsync(CreateClientInputModel input, ApplicationUser user);

        Task<string> AddWorkoutToClientList(string id, string userId);

        Task Delete(string workoutId, string userId);
    }
}
