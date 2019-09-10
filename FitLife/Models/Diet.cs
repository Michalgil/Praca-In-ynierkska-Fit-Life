using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitLife.Models
{
    public class Diet
    {
        public Diet()
        {
            this.Meals = new List<Meal>();
        }
        public int Id { get; set; }
        public int Weight { get; set; }
        public int Kcal { get; set; }
        public int Protein { get; set; }
        public int Fat { get; set; }
        public int Carbohydrates { get; set; }
        public bool IsActive { get; set; }
        public DateTime? Date { get; set; }
        public bool WeightReduction { get; set; }
        public bool Mass { get; set; }
        public bool WeightMaintenance { get; set; }

        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual  ICollection<Meal> Meals { get; set; }

    }
}
