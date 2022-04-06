namespace SoccerCoach.Web.ViewModels.Workouts
{
    using SoccerCoach.Data.Models;
    using SoccerCoach.Services.Mapping;

    public class WorkoutViewModel : IMapFrom<Workout>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string PositionName { get; set; }

        public string VideoUrl { get; set; }

        public Coach AddedByCoach { get; set; }
    }
}
