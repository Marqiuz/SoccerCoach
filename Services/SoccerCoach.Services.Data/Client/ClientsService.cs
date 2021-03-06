namespace SoccerCoach.Services.Data.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SoccerCoach.Common;
    using SoccerCoach.Data.Common.Repositories;
    using SoccerCoach.Data.Models;
    using SoccerCoach.Services.Data.Models;
    using SoccerCoach.Services.Data.Workout;
    using SoccerCoach.Web.ViewModels;
    using SoccerCoach.Web.ViewModels.Workouts;

    public class ClientsService : IClientsService
    {
        private readonly IDeletableEntityRepository<Client> clientRepository;
        private readonly IRepository<WorkoutsList> workoutClientsrepository;

        public ClientsService(
            IDeletableEntityRepository<Client> repository,
            IRepository<WorkoutsList> workoutClientsrepository)
        {
            this.clientRepository = repository;
            this.workoutClientsrepository = workoutClientsrepository;
        }

        public async Task<string> AddWorkoutToClientList(string id, string userId)
        {
            var client = this.GetClient(userId);
            if (id == null)
            {
                return "invalid";
            }

            if (!client.WorkoutsList.Any(x => x.ClientId == client.Id && x.WorkoutId == id))
            {
                var workout = new WorkoutsList { WorkoutId = id, ClientId = client.Id };

                await this.workoutClientsrepository.AddAsync(workout);
                await this.workoutClientsrepository.SaveChangesAsync();
                return "added";
            }

            return "contained";
        }

        public async Task<bool> CreateClientAsync(CreateClientInputModel input, ApplicationUser user)
        {
            var client = new Client
            {
                Name = input.FullName,
                Email = input.Email,
                HasSoccerExperience = input.HasExperience,
                Phone = input.Phone,
                PositionPlayed = input.PositionPlayed,
                User = user,
                UserId = user.Id,
            };

            if (client != null && !string.IsNullOrEmpty(input.Phone))
            {
                await this.clientRepository.AddAsync(client);
                await this.clientRepository.SaveChangesAsync();

                return true;
            }

            throw new InvalidOperationException(GlobalConstants.InvalidOperationExceptionWhileCreatingClient);
        }

        public async Task<IEnumerable<WorkoutInListViewModel>> GetWorkouts(string userId, int page, int itemsPerPage)
        {
            var result = this.workoutClientsrepository
                .AllAsNoTracking()
                .Where(x => x.Client.UserId == userId)
                .Select(x => new WorkoutInListViewModel
                {
                    Id = x.Workout.Id,
                    Name = x.Workout.Name,
                    PositionName = x.Workout.Position.Name,
                    Description = x.Workout.Description,
                    VideoUrl = x.Workout.VideoUrl,
                    AddedByCoach = x.Workout.AddedByCoach,
                })
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .ToList();

            return result;
        }

        // GetClient uses ClientDto so we can get the WorkoutList of the client.
        public ClientDto GetClient(string userId)
        {
            return this.clientRepository.All()
                                 .Where(x => x.UserId == userId)
                                 .Select(x => new ClientDto
                                 {
                                     Id = x.Id,
                                     WorkoutsList = x.WorkoutsList,
                                 })
                                 .First();
        }

        public Client GetClientById(string userId)
        {
            return this.clientRepository.All()
                                 .Where(x => x.UserId == userId)
                                 .FirstOrDefault();
        }

        public int GetCount(string userId)
        {
            var client = this.GetClient(userId);

            return client.WorkoutsList.Count;
        }

        public async Task Delete(string workoutId, string userId)
        {
            var client = this.clientRepository.AllAsNoTracking().FirstOrDefault(x => x.UserId == userId);

            var workout = this.workoutClientsrepository.All()
                .FirstOrDefault(x => x.WorkoutId == workoutId && x.ClientId == client.Id);

            this.workoutClientsrepository.Delete(workout);
            await this.workoutClientsrepository.SaveChangesAsync();
        }
    }
}
