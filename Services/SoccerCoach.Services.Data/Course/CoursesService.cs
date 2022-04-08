namespace SoccerCoach.Services.Data.Course
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using SoccerCoach.Data.Common.Repositories;
    using SoccerCoach.Data.Models;
    using SoccerCoach.Services.Data.Coach;
    using SoccerCoach.Services.Data.Course;
    using SoccerCoach.Services.Mapping;
    using SoccerCoach.Web.ViewModels.Coaches;
    using SoccerCoach.Web.ViewModels.Courses;

    public class CoursesService : ICoursesService
    {
        private readonly ICoachesService coachesService;
        private readonly IDeletableEntityRepository<Course> coursesRepository;

        public CoursesService(
            ICoachesService coachesService,
            IDeletableEntityRepository<Course> coursesRepository)
        {
            this.coachesService = coachesService;
            this.coursesRepository = coursesRepository;
        }

        public async Task CreateCourseAsync(CreateCourseInputModel input, string userId)
        {
            var coach = this.coachesService.GetCoachByUserId(userId);

            var course = new Course
            {
                Name = input.Name,
                PositionName = input.PositionName,
                StarDate = input.StartDate,
                EndDate = input.EndDate,
                Description = input.Description,
                CoachId = coach.Id,
            };

            coach.Courses.Add(course);

            await this.coursesRepository.AddAsync(course);
            await this.coursesRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>(int page, int itemsPerPage)
        {
            var dealerships = this.coursesRepository.AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .To<T>().ToList();
            return dealerships;
        }

        public int GetCount()
        {
            return this.coursesRepository.AllAsNoTracking().Count();
        }
    }
}
