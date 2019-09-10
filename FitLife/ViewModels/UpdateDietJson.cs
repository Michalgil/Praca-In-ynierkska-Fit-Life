using FitLife.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitLife.ViewModels
{
    public class UpdateDietJson
    {
        public int DietId { get; set; }
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
    }
}
