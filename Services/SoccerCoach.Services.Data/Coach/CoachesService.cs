﻿namespace SoccerCoach.Services.Data.Coach
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using SoccerCoach.Common;
    using SoccerCoach.Data.Common.Repositories;
    using SoccerCoach.Data.Models;
    using SoccerCoach.Services.Data.Picture;
    using SoccerCoach.Web.ViewModels;

    public class CoachesService : ICoachesService
    {
        private readonly IDeletableEntityRepository<Coach> coachRepository;

        public CoachesService(
            IDeletableEntityRepository<Coach> coachRepository)
        {
            this.coachRepository = coachRepository;
        }

        public async Task<bool> CreateCoachAsync(CreateCoachInputModel input, ApplicationUser user)
        {
            var coach = new Coach
            {
                Name = input.FullName,
                Email = input.Email,
                Description = input.Description,
                Phone = input.Phone,
                Experience = input.Experience,
                User = user,
                UserId = user.Id,
            };

            if (coach != null && coach.Experience > 0)
            {
                await this.coachRepository.AddAsync(coach);
                await this.coachRepository.SaveChangesAsync();

                return true;
            }

            throw new InvalidOperationException(GlobalConstants.InvalidOperationExceptionWhileCreatingCoach);
        }

        public Coach GetCoachByUserId(string id)
        {
            var coach = this.coachRepository.AllAsNoTracking().FirstOrDefault(x => x.UserId == id);

            return coach;
        }
    }
}
