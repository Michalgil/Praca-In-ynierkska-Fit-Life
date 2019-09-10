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
        public bool IsBegginer { get; set; }
        public bool IsIntermediate { get; set; }
        public bool IsAdvanced { get; set; }
        public DateTime? Date { get; set; }
        public bool IsActive { get; set; }
        public string PriorityPart { get; set; }

        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual ICollection<TrainingExercise> TrainingExercises { get; set; }
    }
}
