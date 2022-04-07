namespace SoccerCoach.Web.ViewModels.Workouts
{
    using SoccerCoach.Data.Models;
    using SoccerCoach.Data.Models.Enums;
    using SoccerCoach.Services.Mapping;

    public class WorkoutInListViewModel : IMapFrom<Workout>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public PositionName PositionName { get; set; }

        public string VideoUrl { get; set; }

        public Coach AddedByCoach { get; set; }
    }
}
