namespace SoccerCoach.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using SoccerCoach.Services.Data;

    public class PlayersController : BaseController
    {
        private readonly IPlayersService playersService;

        public PlayersController(IPlayersService playersService)
        {
            this.playersService = playersService;
        }

        public async Task<IActionResult> All()
        {
            var viewModel = await this.playersService.GetAllPlayersAsync();

            return this.View(viewModel);
        }
    }
}
