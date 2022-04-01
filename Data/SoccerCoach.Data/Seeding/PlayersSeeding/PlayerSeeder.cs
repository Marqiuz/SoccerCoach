namespace SoccerCoach.Data.Seeding.PlayersSeeding
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Newtonsoft.Json;
    using SoccerCoach.Data.Models;
    using SoccerCoach.Data.Models.Enums;

    public class PlayerSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Players.Any())
            {
                return;
            }

            StreamReader reader = new StreamReader(@"wwwroot\Players.json");

            string json = await reader.ReadToEndAsync();

            var playersJson = JsonConvert.DeserializeObject<List<PlayerJsonDTO>>(json);

            var playersList = new List<Player>();

            foreach (var p in playersJson)
            {
                string imageUrl = string.Empty;
                imageUrl = GetPositionImage(p, imageUrl);

                var playerPosition = new Position { Name = (PositionName)Enum.Parse(typeof(PositionName), p.Position) };

                var player = new Player
                {
                    Name = p.Name,
                    TeamName = p.Team,
                    Position = playerPosition,
                    Description = p.Description,
                    PositionId = playerPosition.Id,
                    Trophies = p.Trophies,
                    Height = p.Height,
                    Weight = p.Weight,
                    Experience = p.Experience,
                    ImageUrl = imageUrl,
                };

                playersList.Add(player);
            }

            await dbContext.Players.AddRangeAsync(playersList);
            await dbContext.SaveChangesAsync();
        }

        private static string GetPositionImage(PlayerJsonDTO p, string imageUrl)
        {
            if (p.Name == "Cristiano Ronaldo")
            {
                imageUrl = @"https://img.uefa.com/imgml/TP/players/3/2020/324x324/63706.jpg";
            }
            else if (p.Name == "Sergio Ramos")
            {
                imageUrl = @"https://img.uefa.com/imgml/TP/players/1/2021/324x324/93649.jpg?imwidth=36";
            }
            else if (p.Name == "Neymar Junior")
            {
                imageUrl = @"https://img.uefa.com/imgml/TP/players/1/2022/324x324/250039508.jpg";
            }
            else if (p.Name == "Luka Modric")
            {
                imageUrl = @"https://img.uefa.com/imgml/TP/players/1/2022/324x324/74699.jpg";
            }
            else if (p.Name == "Manuel Neuer")
            {
                imageUrl = @"https://img.uefa.com/imgml/TP/players/3/2020/324x324/97923.jpg";
            }

            return imageUrl;
        }
    }
}
