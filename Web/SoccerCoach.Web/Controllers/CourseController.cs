﻿namespace SoccerCoach.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SoccerCoach.Data.Models;
    using SoccerCoach.Services.Data.Coach;
    using SoccerCoach.Services.Data.Course;
    using SoccerCoach.Web.ViewModels.Coaches;
    using SoccerCoach.Web.ViewModels.Courses;

    public class CourseController : BaseController
    {
        private readonly ICoachesService coachesService;
        private readonly ICoursesService coursesService;
        private readonly UserManager<ApplicationUser> userManager;

        public CourseController(
            ICoachesService coachesService,
            ICoursesService coursesService,
            UserManager<ApplicationUser> userManager)
        {
            this.coachesService = coachesService;
            this.coursesService = coursesService;
            this.userManager = userManager;
        }

        [Authorize(Roles = "Coach")]
        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new CreateCourseInputModel();
            return this.View(viewModel);
        }

        [Authorize(Roles = "Coach")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateCourseInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            await this.coursesService.CreateCourseAsync(input, user.Id);

            return this.Redirect("/");
        }

        [HttpGet]
        public IActionResult All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            const int ItemsPerPage = 9;
            var viewModel = new CoursesListViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                CoursesCount = this.coursesService.GetCount(),
                Courses = this.coursesService.GetAll<CourseInListViewModel>(id, ItemsPerPage),
            };

            return this.View(viewModel);
        }
    }
}
