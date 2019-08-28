using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitLife.Models
{
    public class Training
    {
        public Training()
        {
            this.TrainingExercises = new List<TrainingExercise>();
        }
        public int Id { get; set; }
        public int Break { get; set; }
        public bool isBegginer { get; set; }
        public bool isIntermediate { get; set; }
        public bool isAdvanced { get; set; }
        
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual ICollection<TrainingExercise> TrainingExercises { get; set; }
    }
}
