﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitLife.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NutritionalValue { get; set; }

        public string Category { get; set; }

    }
}
