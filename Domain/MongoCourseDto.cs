using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Api.Domain
{
    public class MongoCourseDto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string LogoPath { get; set; }
        public List<Module> Modules { get; set; }
        public List<Doctor> Doctors { get; set; }
        public List<Student> Students { get; set; }
    }
}
