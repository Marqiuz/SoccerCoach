namespace SoccerCoach.Services.Data.Client
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using SoccerCoach.Data.Models;
    using SoccerCoach.Web.ViewModels;

    public interface IClientsService
    {
        Task<bool> CreateClientAsync(CreateClientInputModel input, ApplicationUser user);
    }
}