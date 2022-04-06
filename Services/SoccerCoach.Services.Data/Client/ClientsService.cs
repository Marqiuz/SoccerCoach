namespace SoccerCoach.Services.Data.Client
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using SoccerCoach.Common;
    using SoccerCoach.Data.Common.Repositories;
    using SoccerCoach.Data.Models;
    using SoccerCoach.Web.ViewModels;

    public class ClientsService : IClientsService
    {
        private readonly IDeletableEntityRepository<Client> clientRepository;

        public ClientsService(IDeletableEntityRepository<Client> repository)
        {
            this.clientRepository = repository;
        }

        public async Task<bool> CreateClientAsync(CreateClientInputModel input, ApplicationUser user)
        {
            var client = new Client
            {
                Name = input.FullName,
                HasSoccerExperience = input.HasExperience,
                Phone = input.Phone,
                PositionPlayed = input.PositionPlayed,
                User = user,
                UserId = user.Id,
            };

            if (client != null)
            {
                await this.clientRepository.AddAsync(client);
                await this.clientRepository.SaveChangesAsync();

                return true;
            }

            throw new InvalidOperationException(GlobalConstants.InvalidOperationExceptionWhileCreatingClient);
        }
    }
}
