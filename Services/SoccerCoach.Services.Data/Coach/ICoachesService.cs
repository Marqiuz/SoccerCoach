namespace SoccerCoach.Services.Data.Coach
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SoccerCoach.Data.Models;
    using SoccerCoach.Web.ViewModels;
    using SoccerCoach.Web.ViewModels.Coaches;

    public interface ICoachesService
    {
        Task<bool> CreateCoachAsync(CreateCoachInputModel input, ApplicationUser user);

        Task<IEnumerable<T>> GetAllCoachesAsync<T>();

        Coach GetCoachByUserId(string id);
    }
}
