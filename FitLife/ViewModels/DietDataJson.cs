using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitLife.ViewModels
{
    public class DietDataJson
    {
        public double Weight { get; set; }
        public double Height { get; set; }
        public double Age { get; set; }
        public double DailyActivity { get; set; }
        public double DietTarget { get; set; }
        public Boolean IsMale { get; set; }

    }
}
