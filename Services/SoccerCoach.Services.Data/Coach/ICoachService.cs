namespace SoccerCoach.Services.Data.Coach
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using SoccerCoach.Web.ViewModels;

    public interface ICoachService
    {
        Task<bool> CreateCoachAsync(CreateCoachInputModel input);
    }
}
