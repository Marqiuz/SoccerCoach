namespace SoccerCoach.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using SoccerCoach.Data.Models;
    using SoccerCoach.Data.Models.Enums;
    using SoccerCoach.Web.ViewModels;

    public interface IPositionsService
    {
        Task<PositionViewModel> GetPlayerAsync(string id);

        Position GetPositionByName(PositionName name);

        Position GetPositionById(string id);
    }
}
