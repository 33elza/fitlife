using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitLife.Models.DBModels
{
    public class Plan
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public ApplicationUser Author { get; set; }
        public string Description { get; set; }
        public ICollection<ApplicationUser> Followers { get; set; }
        public ICollection<Workout> Workouts { get; set; }
    }
}