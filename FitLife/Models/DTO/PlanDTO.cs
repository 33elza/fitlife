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
        public string PlanName { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }       
        public UserProfileDTO Author { get; set; }
      //  public virtual ICollection<UserProfileDTO> Followers { get; set; }
    }
}