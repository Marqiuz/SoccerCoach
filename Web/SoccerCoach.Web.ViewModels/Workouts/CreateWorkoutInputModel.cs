namespace SoccerCoach.Web.ViewModels.Workouts
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using Microsoft.AspNetCore.Http;
    using SoccerCoach.Data.Models.Enums;

    public class CreateWorkoutInputModel
    {
        [Required]
        [Display(Name = "Workout name")]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "For position")]
        public PositionName PositionName { get; set; }

        [Required]
        [Display(Name = "Embed video link")]
        public string VideoUrl { get; set; }
    }
}
