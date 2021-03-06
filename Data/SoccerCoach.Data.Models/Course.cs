namespace SoccerCoach.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    using SoccerCoach.Data.Common.Models;
    using SoccerCoach.Data.Models.Enums;

    public class Course : BaseDeletableModel<string>
    {
        public Course()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Clients = new HashSet<CourseClients>();
        }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public DateTime StarDate { get; set; }

        public DateTime EndDate { get; set; }

        [Required]
        public PositionName PositionName { get; set; }

        [Required]
        [MaxLength(200)]
        public string Description { get; set; }

        [Required]
        [ForeignKey(nameof(Coach))]
        public string CoachId { get; set; }

        [Required]
        public virtual Coach Coach { get; set; }

        public virtual ICollection<CourseClients> Clients { get; set; }
    }
}
