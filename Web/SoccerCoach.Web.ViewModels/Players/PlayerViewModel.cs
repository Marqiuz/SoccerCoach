namespace SoccerCoach.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using SoccerCoach.Data.Models;
    using SoccerCoach.Data.Models.Enums;
    using SoccerCoach.Services.Mapping;

    public class PlayerViewModel : IMapFrom<Player>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public PositionName PositionName { get; set; }

        public string TeamName { get; set; }

        public int Trophies { get; set; }

        public string Height { get; set; }

        public string Weight { get; set; }

        public string Experience { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }
    }
}
