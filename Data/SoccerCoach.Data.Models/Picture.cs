namespace SoccerCoach.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using SoccerCoach.Data.Common.Models;

    public class Picture : BaseDeletableModel<string>
    {
        public Picture()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Coaches = new HashSet<Coach>();
            this.Workouts = new HashSet<Workout>();
        }

        [Required]
        public string Url { get; set; }

        public virtual ICollection<Coach> Coaches { get; set; }

        public virtual ICollection<Workout> Workouts { get; set; }
    }
}
