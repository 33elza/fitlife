using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FitLife.Models.DTO
{
    public class PlanDTO
    {
        public int ID { get; set; }
        public string Url { get; set; }
         [Display(Name = "Название")]
        public string Name { get; set; }
        [Display(Name = "Описание")]
        public string Description { get; set; }
         [Display(Name = "Уровень сложности")]
        public string DifficultyLevel { get; set; }
          [Display(Name = "Пол спортсменов")]
        public string Sex { get; set; }     
        public UserProfileDTO Author { get; set; }

    }
}