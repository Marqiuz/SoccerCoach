namespace SoccerCoach.Data.Seeding.WorkoutsSeeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class PositionWorkoutJsonDTO
    {
        public string Position { get; set; }

        public ICollection<WorkoutJsonDTO> Workouts { get; set; }
    }
}
