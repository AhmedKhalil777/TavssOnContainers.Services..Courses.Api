using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Api.Contracts.V1.Responses
{
    public class MinCourseViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string LogoPath { get; set; }
        public IEnumerable<string> DRID { get; set; }
    }
}
