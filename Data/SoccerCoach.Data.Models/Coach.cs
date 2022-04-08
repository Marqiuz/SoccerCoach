namespace SoccerCoach.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using SoccerCoach.Data.Common.Models;

    public class Coach : BaseDeletableModel<string>
    {
        public Coach()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Courses = new HashSet<Course>();
            this.CoachWorkouts = new HashSet<Workout>();
        }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public int Experience { get; set; }

        public string Description { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        [Required]
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Course> Courses { get; set; }

        public virtual ICollection<Workout> CoachWorkouts { get; set; }
    }
}
