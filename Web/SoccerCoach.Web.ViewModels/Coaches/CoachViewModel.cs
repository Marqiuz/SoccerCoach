namespace SoccerCoach.Web.ViewModels.Coaches
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using SoccerCoach.Data.Models;
    using SoccerCoach.Services.Mapping;

    public class CoachViewModel : IMapFrom<Coach>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int Experience { get; set; }

        public string Description { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public double AverageVote { get; set; }

        public int CoachWorkoutsCount { get; set; }

        public int CoursesCount { get; set; }
    }
}
