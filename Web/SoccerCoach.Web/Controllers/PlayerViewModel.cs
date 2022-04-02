namespace SoccerCoach.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using SoccerCoach.Data.Models.Enums;

    public class PlayerViewModel
    {
        public string Name { get; set; }

        public PositionName PositionName { get; set; }

        public string PositionDescription { get; set; }

        public string PositionPlaystyle { get; set; }

        public string TeamName { get; set; }

        public int Trophies { get; set; }

        public string Height { get; set; }

        public string Weight { get; set; }

        public string Experience { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }
    }
}
