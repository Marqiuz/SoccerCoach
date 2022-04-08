namespace SoccerCoach.Services.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using SoccerCoach.Data.Models;

    public class ClientDto
    {
        public string Id { get; set; }

        public ICollection<WorkoutsList> WorkoutsList { get; set; }
    }
}
