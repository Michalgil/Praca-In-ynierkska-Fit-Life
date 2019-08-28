using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitLife.Models
{
    public class ApplicationUser : IdentityUser
    {
        public Boolean isMale { get; set; }

        public virtual ICollection<Training> trainings { get; set; }

        public virtual ICollection<Diet> Diets { get; set; }
    }
}
