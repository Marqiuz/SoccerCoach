namespace SoccerCoach.Services.Data.Coach
{
    using System.Threading.Tasks;

    using SoccerCoach.Data.Models;
    using SoccerCoach.Web.ViewModels;

    public interface ICoachesService
    {
        Task<bool> CreateCoachAsync(CreateCoachInputModel input, ApplicationUser user);
    }
}
