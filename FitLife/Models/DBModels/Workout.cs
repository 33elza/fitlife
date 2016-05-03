using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitLife.Models.DBModels
{
    public class Workout
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public Plan Plan { get; set; }
        public ICollection<Set> Sets { get; set; }
    }
}