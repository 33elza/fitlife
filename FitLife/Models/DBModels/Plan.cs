using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace FitLife.Models.DBModels
{
    public class Plan
    {
        public int ID { get; set; }
         [Display(Name = "Название")]
        public string Name { get; set; }
        public ApplicationUser Author { get; set; }
        public string AuthorID { get; set; }
         [Display(Name = "Описание")]
        public string Description { get; set; }
         [Display(Name = "Уровень сложности")]
        public string DifficultyLevel { get; set; }
         [Display(Name = "Пол спортсменов")]
        public string Sex { get; set; }
        public ICollection<ApplicationUser> Followers { get; set; }

        [JsonIgnore]
        [IgnoreDataMember] 
        public ICollection<Workout> Workouts { get; set; }
    }
    public enum DifficultyLevel
    {
        High = 1,
        Middle = 2,
        Low = 3
    }
    public enum Sex
    {
        Male = 1,
        Female = 2,
        Both = 3
    }
}