namespace SoccerCoach.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SoccerCoach.Common;
    using SoccerCoach.Data.Common.Repositories;
    using SoccerCoach.Data.Models;
    using SoccerCoach.Services.Data.Client;
    using SoccerCoach.Services.Data.Models;
    using SoccerCoach.Web.ViewModels.Workouts;

    public class ClientController : BaseController
    {
        public const int ItemsPerPage = 9;

        private readonly IClientsService clientsService;
        private readonly UserManager<ApplicationUser> userManager;

        public ClientController(
            IClientsService clientsService,
            UserManager<ApplicationUser> userManager)
        {
            this.clientsService = clientsService;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> AddToWorkoutList(string id)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var result = await this.clientsService.AddWorkoutToClientList(id, user.Id);

            if (result == true)
            {
                this.TempData["SuccessMessage"] = GlobalConstants.SuccessfullyAddedWorkout;
            }
            else
            {
                this.TempData["ErrorMessage"] = GlobalConstants.WorkoutAlreadyAdded;
            }

            return this.RedirectToAction("All", "Workout");
        }

        [HttpGet]
        public async Task<IActionResult> WorkoutsList(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            var user = await this.userManager.GetUserAsync(this.User);

            var viewModel = new ListOfWorkoutsViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                WorkoutsCount = this.clientsService.GetCount(user.Id),
                Workouts = await this.clientsService.GetWorkouts(user.Id, id, ItemsPerPage),
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> Delete(string workoutId)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            await this.clientsService.Delete(workoutId, user.Id);
            return this.RedirectToAction(nameof(this.WorkoutsList));
        }
    }
}
