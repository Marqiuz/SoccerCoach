    namespace SoccerCoach.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SoccerCoach.Data.Models;
    using SoccerCoach.Services.Data.Coach;
    using SoccerCoach.Services.Data.Workout;
    using SoccerCoach.Web.ViewModels.Workouts;

    public class WorkoutController : BaseController
    {
        public const int ItemsPerPage = 9;

        private readonly IWorkoutsService workoutsService;
        private readonly UserManager<ApplicationUser> userManager;

        public WorkoutController(
            IWorkoutsService workoutsService,
            UserManager<ApplicationUser> userManager)
        {
            this.workoutsService = workoutsService;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult All(SearchWorkoutInputModel inputModel, int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            var result = this.workoutsService.GetSearchedPositions<WorkoutInListViewModel>(inputModel, id, ItemsPerPage);
            var viewModel = new ListOfWorkoutsViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                WorkoutsCount = result.Count,
                Workouts = result.Workouts,
                Striker = inputModel.Striker,
                Winger = inputModel.Winger,
                Defender = inputModel.Defender,
                Midfielder = inputModel.Midfielder,
                Goalkeeper = inputModel.Goalkeeper,
            };

            return this.View(viewModel);
        }

        [Authorize(Roles = "Coach")]
        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new CreateWorkoutInputModel();
            return this.View(viewModel);
        }

        [Authorize(Roles = "Coach")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateWorkoutInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            await this.workoutsService.CreateAsync(input, user.Id);

            return this.RedirectToAction("All");
        }

        [HttpGet]
        [Authorize(Roles = "Coach")]
        public IActionResult Edit(string id)
        {
            var result = this.workoutsService.GetWorkoutForEdit(id);

            return this.View(result);
        }

        [HttpPost]
        [Authorize(Roles = "Coach")]
        public async Task<IActionResult> Edit(EditWorkoutViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.workoutsService.EditAsync(input);
            return this.RedirectToAction("All");
        }

        [Authorize(Roles = "Coach")]
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await this.workoutsService.DeleteAsync(id);
            return this.RedirectToAction("All");
        }

        [HttpGet]
        public IActionResult Watch(string id)
        {
            var workout = this.workoutsService.GetWorkoutById(id);
            var viewModel = new WatchViewModel()
            {
                WorkoutName = workout.Name,
                VideoUrl = workout.VideoUrl,
            };

            return this.View(viewModel);
        }
    }
}
