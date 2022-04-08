namespace SoccerCoach.Web.ViewModels.Courses
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using SoccerCoach.Data.Models;
    using SoccerCoach.Data.Models.Enums;
    using SoccerCoach.Services.Mapping;

    public class CourseInListViewModel : IMapFrom<Course>
    {
        public string Name { get; set; }

        public DateTime StarDate { get; set; }

        public DateTime EndDate { get; set; }

        public PositionName PositionName { get; set; }

        public string Description { get; set; }

        public Coach Coach { get; set; }
    }
}
