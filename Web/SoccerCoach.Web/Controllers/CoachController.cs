namespace SoccerCoach.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using SoccerCoach.Services.Data.Coach;
    using SoccerCoach.Web.ViewModels.Coaches;

    public class CoachController : BaseController
    {
        private readonly ICoachesService coachesService;

        public CoachController(ICoachesService coachesService)
        {
            this.coachesService = coachesService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var viewModel = await this.coachesService.GetAllCoachesAsync<CoachViewModel>();

            return this.View(viewModel);
        }
    }
}
