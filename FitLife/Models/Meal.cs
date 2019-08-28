using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitLife.Models
{
    public class Meal
    {
        public int Id { get; set; }
        public int NumberOfMeal { get; set; }
        public int Kcal { get; set; }
        public int Protein { get; set; }
        public int Fat { get; set; }
        public int Carbohydrates { get; set; }

        public int DietId { get; set; }
        public Diet Diet { get; set; }
        
    }
}
