namespace SoccerCoach.Services.Data.Course
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SoccerCoach.Data.Common.Repositories;
    using SoccerCoach.Data.Models;
    using SoccerCoach.Services.Data.Client;
    using SoccerCoach.Services.Data.Coach;
    using SoccerCoach.Services.Data.Course;
    using SoccerCoach.Services.Mapping;
    using SoccerCoach.Web.ViewModels.Coaches;
    using SoccerCoach.Web.ViewModels.Courses;

    public class CoursesService : ICoursesService
    {
        private readonly ICoachesService coachesService;
        private readonly IDeletableEntityRepository<Course> coursesRepository;
        private readonly IClientsService clientsService;
        private readonly IDeletableEntityRepository<CourseClients> courseClientsRepository;

        public CoursesService(
            ICoachesService coachesService,
            IClientsService clientsService,
            IDeletableEntityRepository<CourseClients> courseClientsRepository,
            IDeletableEntityRepository<Course> coursesRepository)
        {
            this.coachesService = coachesService;
            this.clientsService = clientsService;
            this.coursesRepository = coursesRepository;
            this.courseClientsRepository = courseClientsRepository;
        }

        public async Task<string> AddClientToCourse(string id, string userId)
        {
            var client = this.clientsService.GetClientById(userId);

            if (!this.courseClientsRepository.AllAsNoTracking().Any(x => x.ClientId == client.Id && x.CourseId == id))
            {
                var courseApplication = new CourseClients
                {
                    ClientId = client.Id,
                    CourseId = id,
                };

                await this.courseClientsRepository.AddAsync(courseApplication);
                await this.courseClientsRepository.SaveChangesAsync();

                return "created";
            }

            return "contained";
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

        public async Task<IEnumerable<CourseInListViewModel>> GetAll(string userId, int page, int itemsPerPage)
        {
            var client = this.clientsService.GetClientById(userId);

            var courses = this.coursesRepository.AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .Select(x => new CourseInListViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    HasApplied = client == null ? false : this.courseClientsRepository.All().Any(c => c.ClientId == client.Id && c.CourseId == x.Id),
                    PositionName = x.PositionName,
                    StartDate = x.StarDate,
                    EndDate = x.EndDate,
                    Description = x.Description,
                    Coach = x.Coach,
                })
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                 .ToList();

            return courses;
        }

        public int GetCount()
        {
            return this.coursesRepository.AllAsNoTracking().Count();
        }
    }
}
