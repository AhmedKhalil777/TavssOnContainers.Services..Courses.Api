using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Api.Domain
{
    public class Module
    {
        [Required]
        public string Id { get; set; }
        public string Name { get; set; }
        public string LogoPath { get; set; }
        public int Position { get; set; }
        public List<Topic> Topics { get; set; }
    }
}
