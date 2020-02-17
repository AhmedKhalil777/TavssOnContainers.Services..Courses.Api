using Cosmonaut.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Api.Domain
{
    [CosmosCollection("courses")]
    public class CosmosCourseDto
    {
        [CosmosPartitionKey]
        [JsonProperty("id")]
        public string Id { get; set; }
        public string Name { get; set; }
        public string LogoPath { get; set; }
        public List<Module> Modules { get; set; }
        public List<Doctor> Doctors { get; set; }
        public List<Student> Students { get; set; }
    }
}
