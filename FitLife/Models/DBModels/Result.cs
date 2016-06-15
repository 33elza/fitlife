using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FitLife.Models.DBModels
{
    public class Result
    {
        public int ID { get; set; }
        public int Weight { get; set; }
        public int Quantity { get; set; }
        public int Time { get; set; }
        public string Note { get; set; }
        public Set Set { get; set; }
        [Required]
        public int SetID { get; set; }
        public string UserID { get; set; }
    }
}