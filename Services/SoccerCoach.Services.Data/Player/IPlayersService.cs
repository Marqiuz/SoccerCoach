namespace SoccerCoach.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using SoccerCoach.Web.Controllers;

    public interface IPlayersService
    {
        Task<IEnumerable<T>> GetAllPlayersAsync<T>();
    }
}
