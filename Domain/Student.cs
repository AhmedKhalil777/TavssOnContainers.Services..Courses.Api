using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Api.Domain
{
    public class Student 
    {
        [Required]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Mark { get; set; }
    }
}
