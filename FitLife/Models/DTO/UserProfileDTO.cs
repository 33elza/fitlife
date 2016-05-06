using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitLife.Models.DTO
{
    public class UserProfileDTO
    {
        public string ID { get; set; }
        public string email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Info { get; set; }
    }
}