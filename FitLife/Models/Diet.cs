using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitLife.Models
{
    public class Diet
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int Kcal { get; set; }
        public int Protein { get; set; }
        public int Fat { get; set; }
        public int Carbohydrates { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual  ICollection<Meal> Meals { get; set; }

    }
}
