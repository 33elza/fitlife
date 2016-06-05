using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitLife.Models.DTO
{
    public class PlanDTO
    {
        public int ID { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DifficultyLevel { get; set; }
        public string Sex { get; set; }     
        public UserProfileDTO Author { get; set; }

    }
}