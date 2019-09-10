using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitLife.Models
{
    public class Dimensions
    {
        public int Id { get; set; }

        public float Arms { get; set; }
        public float Chest { get; set; }
        public float Thig { get; set; }
        public float Waist { get; set; }
        public float Buttocks { get; set; }
        public Boolean isActive { get; set; }
        public DateTime?  Date { get; set; }
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
