using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitLife.Models
{
    public class PartOfBody
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Exercise> Exercises { get; set; }
    }
}
