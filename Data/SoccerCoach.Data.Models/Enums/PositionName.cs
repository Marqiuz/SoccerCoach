namespace SoccerCoach.Data.Models.Enums
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public enum PositionName
    {
        [Display(Name = "Striker")]
        Striker = 0,
        [Display(Name = "Winger")]
        Winger = 1,
        [Display(Name = "Midfielder")]
        Midfielder = 2,
        [Display(Name = "Defender")]
        Defender = 3,
        [Display(Name = "Goalkeeper")]
        Goalkeeper = 4,
    }
}
