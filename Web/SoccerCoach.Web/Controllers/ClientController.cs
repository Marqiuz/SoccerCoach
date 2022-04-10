namespace SoccerCoach.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
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
        public const int ItemsPerPage = 6;

        private readonly IClientsService clientsService;
        private readonly UserManager<ApplicationUser> userManager;

        public ClientController(
            IClientsService clientsService,
            UserManager<ApplicationUser> userManager)
        {
            this.clientsService = clientsService;
            this.userManager = userManager;
        }

        [Authorize(Roles = GlobalConstants.ClientRoleName)]
        [HttpGet]
        public async Task<IActionResult> AddToWorkoutList(string id)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var result = await this.clientsService.AddWorkoutToClientList(id, user.Id);

            if (result == "added")
            {
                this.TempData["SuccessMessage"] = GlobalConstants.SuccessfullyAddedWorkout;
            }
            else if (result == "invalid")
            {
                this.TempData["InvalidMessage"] = GlobalConstants.InvalidWorkout;
            }
            else
            {
                this.TempData["ErrorMessage"] = GlobalConstants.WorkoutAlreadyAdded;
            }

            return this.RedirectToAction("All", "Workout");
        }

        [Authorize(Roles = GlobalConstants.ClientRoleName)]
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

        [Authorize(Roles = GlobalConstants.ClientRoleName)]
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            await this.clientsService.Delete(id, user.Id);
            return this.RedirectToAction(nameof(this.WorkoutsList));
        }
    }
}
