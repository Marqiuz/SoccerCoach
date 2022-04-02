namespace SoccerCoach.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using SoccerCoach.Data.Seeding.PlayersSeeding;
    using SoccerCoach.Data.Seeding.PositionsSeeding;
    using SoccerCoach.Data.Seeding.WorkoutsSeeding;

    public class ApplicationDbContextSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            var logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger(typeof(ApplicationDbContextSeeder));

            var seeders = new List<ISeeder>
                          {
                              new RolesSeeder(),
                              new SettingsSeeder(),
                              new PlayerSeeder(),
                              new PositionSeeder(),
                              new WorkoutSeeder(),
                          };

            foreach (var seeder in seeders)
            {
                await seeder.SeedAsync(dbContext, serviceProvider);
                await dbContext.SaveChangesAsync();
                logger.LogInformation($"Seeder {seeder.GetType().Name} done.");
            }
        }
    }
}
