namespace SoccerCoach.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using SoccerCoach.Data.Common.Models;
    using SoccerCoach.Data.Models.Enums;

    public class Position : BaseDeletableModel<string>
    {
        public Position()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Workouts = new HashSet<Workout>();
            this.Players = new HashSet<Player>();
        }

        public PositionName Name { get; set; }

        public string Description { get; set; }

        public string Playstyle { get; set; }

        public virtual ICollection<Player> Players { get; set; }

        public virtual ICollection<Workout> Workouts { get; set; }
    }
}
