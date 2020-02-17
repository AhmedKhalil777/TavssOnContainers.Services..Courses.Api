using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Api.Contracts.V1.Requests
{
    public class StudentViewModel
    {
        [Required]
        public string Id { get; set; }
        public string Name { get; set; }
        [Range(0,100)]
        public float Mark { get; set; }
    }
}
