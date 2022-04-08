namespace SoccerCoach.Web.ViewModels.Workouts
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class SearchWorkoutInputModel
    {
        [Display(Name = "Striker")]
        public bool Striker { get; set; }

        [Display(Name = "Winger")]
        public bool Winger { get; set; }

        [Display(Name = "Defender")]
        public bool Defender { get; set; }

        [Display(Name = "Midfielder")]
        public bool Midfielder { get; set; }

        [Display(Name = "Goalkeeper")]
        public bool Goalkeeper { get; set; }
    }
}
