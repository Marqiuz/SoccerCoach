namespace SoccerCoach.Services.Data.Coach
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using SoccerCoach.Common;
    using SoccerCoach.Data.Common.Repositories;
    using SoccerCoach.Data.Models;
    using SoccerCoach.Services.Data.Picture;
    using SoccerCoach.Web.ViewModels;

    public class CoachService : ICoachService
    {
        private readonly IDeletableEntityRepository<Coach> coachRepository;
        private readonly IPictureService pictureService;

        public CoachService(
            IDeletableEntityRepository<Coach> coachRepository,
            IPictureService pictureService)
        {
            this.coachRepository = coachRepository;
            this.pictureService = pictureService;
        }

        public async Task<bool> CreateCoachAsync(CreateCoachInputModel input)
        {
            var coach = new Coach
            {
                Name = input.FullName,
                Email = input.Email,
                Description = input.Description,
                Phone = input.Phone,
                Experience = input.Experience,
            };

            if (coach != null && coach.Experience > 0)
            {
                await this.coachRepository.AddAsync(coach);
                await this.coachRepository.SaveChangesAsync();

                return true;
            }

            throw new InvalidOperationException(GlobalConstants.InvalidOperationExceptionWhileCreatingCoach);
        }
    }
}