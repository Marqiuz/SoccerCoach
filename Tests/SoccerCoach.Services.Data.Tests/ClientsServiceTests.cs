namespace SoccerCoach.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Moq;
    using SoccerCoach.Data;
    using SoccerCoach.Data.Common.Repositories;
    using SoccerCoach.Data.Models;
    using SoccerCoach.Data.Models.Enums;
    using SoccerCoach.Data.Repositories;
    using SoccerCoach.Services.Data.Client;
    using SoccerCoach.Web.ViewModels;
    using Xunit;

    public class ClientsServiceTests
    {
        private readonly Mock<IRepository<WorkoutsList>> workoutslistRepository;
        private readonly Mock<IDeletableEntityRepository<Client>> clientRepository;
        private readonly ClientsService clientsService;

        public ClientsServiceTests()
        {
            this.workoutslistRepository = new Mock<IRepository<WorkoutsList>>();
            this.clientRepository = new Mock<IDeletableEntityRepository<Client>>();
            this.clientsService = new ClientsService(this.clientRepository.Object, this.workoutslistRepository.Object);
        }

        [Fact]
        public async Task CreateClientShouldWork()
        {
            var clients = new List<Client>();
            this.clientRepository.Setup(r => r.AllAsNoTracking()).Returns(() => clients.AsQueryable());

            this.clientRepository.Setup(r => r.AddAsync(It.IsAny<Client>())).Callback((Client client) => clients.Add(client));

            var model = new CreateClientInputModel
            {
                FullName = "Number One",
                PositionPlayed = PositionName.Midfielder,
                Email = "asdasd@gmail.com",
                HasExperience = true,
                Phone = "088355353",
            };

            var appUser = new ApplicationUser { Id = "clientuserId", Email = "asdasd@gmail.com" };

            await this.clientsService.CreateClientAsync(model, appUser);

            Assert.Contains(clients, x => x.Name == "Number One");
            Assert.Single(clients);
            this.clientRepository.Verify(x => x.AllAsNoTracking(), Times.Never);
        }

        [Fact]
        public async Task CreateClientShouldThrowInvalidData()
        {
            var clients = new List<Client>();
            this.clientRepository.Setup(r => r.AllAsNoTracking()).Returns(() => clients.AsQueryable());

            this.clientRepository.Setup(r => r.AddAsync(It.IsAny<Client>())).Callback((Client client) => clients.Add(client));

            var model = new CreateClientInputModel
            {
                FullName = string.Empty,
                PositionPlayed = PositionName.Midfielder,
                Email = string.Empty,
                HasExperience = false,
                Phone = string.Empty,
            };

            var appUser = new ApplicationUser { Id = string.Empty, Email = string.Empty };

            await Assert.ThrowsAsync<InvalidOperationException>(() => this.clientsService.CreateClientAsync(model, appUser));
        }

        [Fact]
        public async Task AddWorkoutToClientListShouldReturnAdded()
        {
            var workoutsList = new List<WorkoutsList>();
            this.workoutslistRepository.Setup(r => r.AllAsNoTracking()).Returns(() => workoutsList.AsQueryable());

            this.workoutslistRepository.Setup(r => r.AddAsync(It.IsAny<WorkoutsList>())).Callback((WorkoutsList workout) => workoutsList.Add(workout));

            var clients = new List<Client>
            {
                 new Client
                {
                    Id = "client1",
                    Name = "Pesho",
                    Phone = "088",
                    HasSoccerExperience = true,
                    PositionPlayed = PositionName.Midfielder,
                    User = new ApplicationUser { Id = "clientUser"},
                    UserId = "clientUser",
                },
            };

            this.clientRepository.Setup(r => r.All()).Returns(() => clients.AsQueryable());

            this.clientRepository.Setup(r => r.AddAsync(It.IsAny<Client>())).Callback((Client client) => clients.Add(client));

            var result = await this.clientsService.AddWorkoutToClientList("w1", "clientUser");
            Assert.Equal("added", result);
            Assert.Single(workoutsList);
            this.workoutslistRepository.Verify(x => x.AllAsNoTracking(), Times.Never);
        }

        [Fact]
        public async Task AddWorkoutToClientListShouldContainedIfOneAlreadyExists()
        {
            var workoutsList = new List<WorkoutsList>
            {
                new WorkoutsList
                {
                    ClientId = "client1",
                    WorkoutId = "w1",
                },
            };

            this.workoutslistRepository.Setup(r => r.AllAsNoTracking()).Returns(() => workoutsList.AsQueryable());

            this.workoutslistRepository.Setup(r => r.AddAsync(It.IsAny<WorkoutsList>())).Callback((WorkoutsList workout) => workoutsList.Add(workout));

            var clients = new List<Client>
            {
                 new Client
                {
                    Id = "client1",
                    Name = "Pesho",
                    Phone = "088",
                    HasSoccerExperience = true,
                    PositionPlayed = PositionName.Midfielder,
                    User = new ApplicationUser { Id = "clientUser"},
                    UserId = "clientUser",
                    WorkoutsList = new List<WorkoutsList>
                                  {
                                   new WorkoutsList
                                   {
                                       ClientId = "client1",
                                       WorkoutId = "w1",
                                   },
                                  },
                },
            };

            this.clientRepository.Setup(r => r.All()).Returns(() => clients.AsQueryable());

            this.clientRepository.Setup(r => r.AddAsync(It.IsAny<Client>())).Callback((Client client) => clients.Add(client));

            var result = await this.clientsService.AddWorkoutToClientList("w1", "clientUser");
            Assert.Equal("contained", result);
            Assert.Single(workoutsList);
            this.workoutslistRepository.Verify(x => x.AllAsNoTracking(), Times.Never);
        }

        [Fact]
        public async Task AddWorkoutToClientListShouldReturnInvalidIfWorkoutIdIsNull()
        {
            var workoutsList = new List<WorkoutsList>();
            this.workoutslistRepository.Setup(r => r.AllAsNoTracking()).Returns(() => workoutsList.AsQueryable());

            this.workoutslistRepository.Setup(r => r.AddAsync(It.IsAny<WorkoutsList>())).Callback((WorkoutsList workout) => workoutsList.Add(workout));

            var clients = new List<Client>
            {
                 new Client
                {
                    Id = "client1",
                    Name = "Pesho",
                    Phone = "088",
                    HasSoccerExperience = true,
                    PositionPlayed = PositionName.Midfielder,
                    User = new ApplicationUser { Id = "clientUser"},
                    UserId = "clientUser",
                },
            };

            this.clientRepository.Setup(r => r.All()).Returns(() => clients.AsQueryable());

            this.clientRepository.Setup(r => r.AddAsync(It.IsAny<Client>())).Callback((Client client) => clients.Add(client));

            var result = await this.clientsService.AddWorkoutToClientList(null, "clientUser");
            Assert.Equal("invalid", result);
            Assert.Empty(workoutsList);
            this.workoutslistRepository.Verify(x => x.AllAsNoTracking(), Times.Never);
        }

        [Fact]
        public async Task GetWorkoutsShouldReturnAllWorkoutsAddedInClientsList()
        {
            var client = new Client
            {
                Id = "client1",
                Name = "Pesho",
                Phone = "088",
                HasSoccerExperience = true,
                PositionPlayed = PositionName.Midfielder,
                User = new ApplicationUser { Id = "clientUser" },
                UserId = "clientUser",
                WorkoutsList = new List<WorkoutsList>
                                  {
                                   new WorkoutsList
                                   {
                                       ClientId = "client1",
                                       WorkoutId = "w1",
                                   },
                                  },
            };

            var workoutsList = new List<WorkoutsList>
            {
                  new WorkoutsList
                                   {
                                       ClientId = "client1",
                                       Client = client,
                                       WorkoutId = "w1",
                                       Workout = new Workout
                                       {
                                           Id = "w1",
                                           Name = "Workout One",
                                           Description = "Try this moves",
                                           Position = new Position
                                           {
                                                Id = "PositionOne",
                                                Name = PositionName.Midfielder,
                                                Description = "this is for strikers",
                                                Playstyle = "You play striker",
                                           },
                                           VideoUrl = "test youtube link",
                                           PositionId = "PositionOne",
                                           Picture = new Picture { Id = "pic", Url = "test url" },
                                           PictureId = "pic",
                                           ImageUrl = "testing",
                                           AddedByCoach = new Coach
                                           {
                                               Id = "c1",
                                               Name = "Coach1",
                                               Description = "desc1",
                                               Experience = 2,
                                               Phone = "0884252",
                                               Email = "coach123@abv.bg",
                                               User = new ApplicationUser { Id = "coachuser" },
                                               UserId = "coachuser",
                                           },
                                       },
                                   },
            };
            this.workoutslistRepository.Setup(r => r.AllAsNoTracking()).Returns(() => workoutsList.AsQueryable());

            var clients = new List<Client> { client };

            this.clientRepository.Setup(r => r.All()).Returns(() => clients.AsQueryable());

            var result = await this.clientsService.GetWorkouts("clientUser", 1, 12);
            Assert.Single(result);
            Assert.Equal("test youtube link", result.ToList().First().VideoUrl);
            Assert.Equal(PositionName.Midfielder, result.ToList().First().PositionName);
            this.workoutslistRepository.Verify(x => x.AllAsNoTracking(), Times.Once);
        }

        [Fact]
        public void GetCountShouldReturnCorrectClientWorkoutsCount()
        {
            var client = new Client
            {
                Id = "client1",
                Name = "Pesho",
                Phone = "088",
                HasSoccerExperience = true,
                PositionPlayed = PositionName.Midfielder,
                User = new ApplicationUser { Id = "clientUser" },
                UserId = "clientUser",
                WorkoutsList = new List<WorkoutsList>
                                  {
                                   new WorkoutsList
                                   {
                                       ClientId = "client1",
                                       WorkoutId = "w1",
                                   },
                                  },
            };

            var clients = new List<Client> { client };

            this.clientRepository.Setup(r => r.All()).Returns(() => clients.AsQueryable());

            var result = this.clientsService.GetCount("clientUser");
            Assert.Equal(1, result);
            this.clientRepository.Verify(x => x.All(), Times.Once);
        }

        [Fact]
        public async Task DeleteShouldWorkCorrectlyWithExistingWorkoutInClientsList()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var client = new Client
            {
                Id = "client1",
                Name = "Pesho",
                Phone = "088",
                HasSoccerExperience = true,
                PositionPlayed = PositionName.Midfielder,
                User = new ApplicationUser { Id = "clientUser" },
                UserId = "clientUser",
            };

            var workoutList = new WorkoutsList
            {
                ClientId = "client1",
                Client = client,
                WorkoutId = "w1",
                Workout = new Workout
                {
                    Id = "w1",
                    Name = "Workout One",
                    Description = "Some kind of workout description",
                    Position = new Position
                    {
                        Id = "PositionOne",
                        Name = PositionName.Striker,
                        Description = "this is for strikers",
                        Playstyle = "You play strike",
                    },
                    VideoUrl = "test youtube link",
                    PositionId = "PositionOne",
                    Picture = new Picture { Id = "pic", Url = "test url" },
                    PictureId = "pic",
                    ImageUrl = "testimg",
                },
            };

            var clientsRepo = new EfDeletableEntityRepository<Client>(dbContext);
            var wlistRepo = new EfDeletableEntityRepository<WorkoutsList>(dbContext);

            var clientsDbService = new ClientsService(clientsRepo, wlistRepo);

            await dbContext.Clients.AddAsync(client);
            await dbContext.WorkoutsList.AddAsync(workoutList);
            await dbContext.SaveChangesAsync();

            await clientsDbService.Delete("w1", "clientUser");
            var result = wlistRepo.All().Where(x => x.ClientId == "client1")
                                 .FirstOrDefault();
            Assert.Null(result);
        }
    }
}
