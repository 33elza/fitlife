using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FitLife.Models.DBModels
{
    public class Exercise
    {
        [Key]
        public int ID { get; set; }
         [Display(Name = "Название")]
        public string ExcerciseName { get; set; }
         [Display(Name = "Описание")]
        public string Description { get; set; }
        public string AuthorID { get; set; }
        public ApplicationUser Author { get; set; }

    }
}