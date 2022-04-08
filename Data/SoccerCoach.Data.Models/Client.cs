namespace SoccerCoach.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using SoccerCoach.Data.Common.Models;
    using SoccerCoach.Data.Models.Enums;

    public class Client : BaseDeletableModel<string>
    {
        public Client()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Courses = new HashSet<CourseClients>();
            this.WorkoutsList = new HashSet<WorkoutsList>();
        }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public bool HasSoccerExperience { get; set; }

        [Required]
        public PositionName PositionPlayed { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        [Required]
        public virtual ApplicationUser User { get; set; }

        [Required]
        public string Email { get; set; }

        public virtual ICollection<CourseClients> Courses { get; set; }

        public virtual ICollection<WorkoutsList> WorkoutsList { get; set; }
    }
}
