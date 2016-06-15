using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FitLife.Models.DBModels
{
    public class Workout
    {
        public int ID { get; set; }
         [Display(Name = "Дата")]
        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }

        [Required]
        [Display(Name = "Описание")]
        public string Description { get; set; }       
        public Plan Plan { get; set; }
        [Required]
        public int PlanID { get; set; }
        public ICollection<Set> Sets { get; set; }
    }
}