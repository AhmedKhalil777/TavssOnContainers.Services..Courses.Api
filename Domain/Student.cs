﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Api.Domain
{
    public class Student 
    {
        [Required]
        public string Id { get; set; }
        public string Name { get; set; }
        public int Mark { get; set; }
    }
}
