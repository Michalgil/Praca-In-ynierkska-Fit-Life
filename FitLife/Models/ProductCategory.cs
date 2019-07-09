using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitLife.Models
{
    public class ProductCategory
    {
        public int Id { get; set; }
        public string CategoryName {get;set;}
        public virtual ICollection<Product> Products { get; set; }
    }
}
