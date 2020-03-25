using Course.Api.Domain;
using Course.Api.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.IO;


namespace Course.Api.Seeding
{
    public static class Seeder
    {
        public async static void CoursesSeeding(this IMongoCollection<MongoCourseDto>  collection, ICoursestoreDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            collection = database.GetCollection<MongoCourseDto>(settings.CoursesCollectionName);
            var courses = await collection.FindAsync(x=> true );
            if (courses.FirstOrDefault() == null)
            {
                using(var reader = new StreamReader("CoureseSeeder.json"))
                {

                    var Courses = Newtonsoft.Json.JsonConvert.DeserializeObject<List<MongoCourseDto>>(reader.ReadToEnd());
                    collection.InsertManyAsync(Courses).Wait();
                }
            }

        }
    }
}
