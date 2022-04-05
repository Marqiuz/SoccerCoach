namespace SoccerCoach.Services.Data.Coach
{
    using System.Threading.Tasks;

    using SoccerCoach.Data.Models;
    using SoccerCoach.Web.ViewModels;

    public interface ICoachService
    {
        Task<bool> CreateCoachAsync(CreateCoachInputModel input, ApplicationUser user);
    }
}
