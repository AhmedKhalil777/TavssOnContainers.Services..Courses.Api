using Identity.Api.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Api.Contracts.V1.Requests
{
    public class CreateTopicViewModel
    {
        public string Discription { get; set; }
        public topicType Type { get; set; }
        public string Name { get; set; }
        public int Position { get; set; }
    }
}
