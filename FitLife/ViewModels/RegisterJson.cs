﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FitLife
{
    public class RegisterJson
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public Boolean IsMale { get; set; }
        public string Password { get; set; }


        
    }
}
