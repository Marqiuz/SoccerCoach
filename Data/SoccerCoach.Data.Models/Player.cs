namespace SoccerCoach.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    using SoccerCoach.Data.Common.Models;

    public class Player : BaseDeletableModel<string>
    {
        public Player()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [ForeignKey(nameof(Position))]
        public string PositionId { get; set; }

        [Required]
        public virtual Position Position { get; set; }

        [Required]
        [MaxLength(80)]
        public string TeamName { get; set; }

        [Range(0, 10)]
        public int Trophies { get; set; }

        [Required]
        public string Height { get; set; }

        [Required]
        public string Weight { get; set; }

        [Required]
        public string Experience { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }
    }
}
