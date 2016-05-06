using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitLife.Models.DTO
{
    public class WorkoutDTO
    {
        public int ID { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public PlanDTO Plan { get; set; }
    }
}