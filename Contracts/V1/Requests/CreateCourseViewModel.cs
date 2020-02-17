using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Api.Contracts.V1.Requests
{
    public class CreateCourseViewModel
    {
        
        public string Name { get; set; }
        public string DrId { get; set; }
        public string DrName { get; set; }
    }
}
