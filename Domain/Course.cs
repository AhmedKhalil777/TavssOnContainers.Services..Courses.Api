using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Api.Domain
{
    public class Courses
    {
        [Required]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LogoPath { get; set; }
        public List<Module> modules { get; set; }
        public List<Doctor> Doctors { get; set; }
        public List<Student> Students { get; set; }
    }
}
