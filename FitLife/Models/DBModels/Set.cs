using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FitLife.Models.DBModels
{
    public class Set
    {
        public int ID { get; set; }
         [Display(Name = "Вес")]
        public int? Weight { get; set; }
         [Display(Name = "Количество повторений")]
        public int? Quantity { get; set; }
         [Display(Name = "Время выполнения")]
        public int? Time { get; set; }
         [Display(Name = "Описание")]
        public string Description { get; set; }
           [Display(Name = "Упражнение")]
        public Exercise Exercise { get; set; }
        public int ExerciseID { get; set; }
        public Workout Workout { get; set; }
        public int WorkoutID { get; set; }
        public int ResultID { get; set; }

    }
}