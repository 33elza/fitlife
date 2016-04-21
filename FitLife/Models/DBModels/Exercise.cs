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
    }
}