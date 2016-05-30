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
        public string ExcerciseName { get; set; }
        public string Description { get; set; }
        public string AuthorID { get; set; }
        public ApplicationUser Author { get; set; }

    }
}