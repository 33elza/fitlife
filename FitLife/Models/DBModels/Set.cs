using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitLife.Models.DBModels
{
    public class Set
    {
        public int ID { get; set; }
        public int Weight { get; set; }
        public int Quantity { get; set; }
        public int Time { get; set; }
        public string Description { get; set; }
        public Exercise Exercise { get; set; }
        public Workout Workout { get; set; }
        public int WorkoutID { get; set; }

    }
}