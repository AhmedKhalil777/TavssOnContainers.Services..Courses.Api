using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Identity.Api.Contracts.V1.Requests;
using Identity.Api.Contracts.V1.Responses;
using Identity.Api.Domain;
using Identity.Api.Options;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Driver;

using Newtonsoft.Json;

namespace Identity.Api.Services
{

    public class MongoCourseService : IMongoCourseService
    {
        private readonly IMongoCollection<MongoCourseDto> _course;
        private readonly IHostingEnvironment _hostingEnvironment; 

        public MongoCourseService(ICoursestoreDatabaseSettings settings , IHostingEnvironment hostingEnvironment)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _course = database.GetCollection<MongoCourseDto>(settings.CoursesCollectionName);
            _hostingEnvironment = hostingEnvironment;
        }

        #region Get
        public async Task<IEnumerable<MinCourseViewModel>> GetCoursesForDoctor(string DID)
        {
            var courses = await _course.FindAsync(x => true);
            var css = courses.ToEnumerable();
            var c = new List<MongoCourseDto>();

            foreach (var course in css)
            {
                var doctors = course.Doctors.SingleOrDefault(x => x.Id == DID);
                if (doctors != null)
                {
                    c.Add(course);
                }
            }
            var DCourses = c.Select(x =>
            new MinCourseViewModel { Id = x.Id, Name = x.Name, LogoPath = x.LogoPath });

            return DCourses;

        }

        public async Task<IEnumerable<MinCourseViewModel>> GetCoursesForStudent(string SID)
        {
            var courses = await _course.FindAsync(x => true);
            var css = courses.ToEnumerable();
            var c = new List<MongoCourseDto>();

            foreach (var course in css)
            {
                var student = course.Students.SingleOrDefault(x => x.Id == SID);
                if (student != null)
                {
                    c.Add(course);
                }
            }
            var SCourses = c.Select(x =>
            new MinCourseViewModel { Id = x.Id, Name = x.Name, LogoPath = x.LogoPath });

            return SCourses;
        }


        public async Task<MongoCourseDto> GetCourse(string CID)
        {
            var course = await _course.FindAsync(x => x.Id == CID);
            return course.SingleOrDefault();
        }

        public async Task<IEnumerable<MinCourseViewModel>> GetCourses()
        {
            var courses = await _course.FindAsync(a => true);
            return courses.ToEnumerable().Select(x =>
                        new MinCourseViewModel { Id = x.Id, Name = x.Name, DRID = x.Doctors.Select(y => y.Id), LogoPath = x.LogoPath });
        }

        public async Task<List<Doctor>> GetCourseDoctors(string CID)
        {
            var course = await GetCourse(CID);
            return course.Doctors;

        }

        public async Task<List<Student>> GetCourseStudents(string CID)
        {
            var course = await GetCourse(CID);
            return course.Students;
        }


        public async Task<Module> GetModuleById(string CID, string MID)
        {
            var course = await GetCourse(CID);
            return course.Modules.SingleOrDefault(x => x.Id == MID);

        }

        public async Task<Topic> GetTopicById(string CID, string MID, string TID)
        {
            var course = await GetCourse(CID);
            var module = course.Modules.SingleOrDefault(x => x.Id == MID);
            return module.Topics.SingleOrDefault(x => x.Id == TID);
        }
        #endregion

        #region Creating
        public async Task<bool> CreateCourse(CreateCourseViewModel course)
        {
            var newcourse = new MongoCourseDto
            {
                Doctors = new List<Doctor>() { new Doctor { Id = course.DrId, Name = course.DrName } },
                LogoPath = null,
                Modules = new List<Module>(),
                Name = course.Name,
                Students = new List<Student>()
            };
            await _course.InsertOneAsync(newcourse);
            return true;
        }

        public async Task<bool> CreateModule(string CID, CreateModuleViewModel model)
        {
            var course = await GetCourse(CID);
            var module = new Module
            {
                Id = Guid.NewGuid().ToString(),
                LogoPath = null,
                Name = model.Name,
                Position = model.Position,
                Topics = new List<Topic>()
            };
            course.Modules.Add(module);
            var result=await _course.ReplaceOneAsync(x=>x.Id==CID,course);
            return result.IsAcknowledged;
        }

        public async Task<bool> CreateTopic(string CID, string MID, CreateTopicViewModel model)
        {
            var course = await GetCourse(CID);
            var module = await GetModuleById(CID, MID);
            
            var topic = new Topic
            {
                Id = Guid.NewGuid().ToString(),
                Discription = model.Discription,
                Name = model.Name,
                Position = model.Position,
                Path = null,
                Type = model.Type
            };
            course.Modules.SingleOrDefault(x => x.Id == MID).Topics.Add(topic);
            var result = await _course.ReplaceOneAsync(x => x.Id == CID, course);
            return result.IsAcknowledged;
        }
        public async Task<bool> AssignDoctorToCourse(string CID, Doctor doctor)
        {
            if (doctor != null)
            {
                var course = await GetCourse(CID);
                course.Doctors.Add(doctor);
                var result = await _course.ReplaceOneAsync(x => x.Id == CID, course);
                return result.IsAcknowledged;
            }
            return false;
        }

        public async Task<bool> AssignStudentToCourse(string CID, Student student)
        {
            if (student != null)
            {
                var course = await GetCourse(CID);
                course.Students.Add(new Student { Id = student.Id, Mark = student.Mark.ToString(), Name = student.Name });
                var result = await _course.ReplaceOneAsync(x => x.Id == CID, course);
                return result.IsAcknowledged;
            }
            return false;
        }



        #endregion

        #region Update
        public async Task<bool> UpdateCourse(UpdateCourseViewModel course)
        {
            if (course == null)
            {
                return false;
            }
            var OldCourse = await GetCourse(course.Id);
            OldCourse.Name = course.Name;
            OldCourse.Doctors.SingleOrDefault().Id = course.DrId;
            OldCourse.Doctors.SingleOrDefault().Name = course.Name;
            var result = await _course.ReplaceOneAsync(x => x.Id == course.Id, OldCourse);
            return result.IsAcknowledged;
        }
        public async Task<bool> UpdateModule(string CID, string MID, CreateModuleViewModel model)
        {

            var course = await GetCourse(CID);
            var module = course.Modules.SingleOrDefault(x => x.Id == MID);
            var umodule = new Module { Id = module.Id, LogoPath = module.LogoPath, Name = model.Name, Position = model.Position, Topics = module.Topics };
            var result1 = course.Modules.Remove(module);
            if (result1)
            {
                course.Modules.Add(umodule);
                var updated = await _course.ReplaceOneAsync(x => x.Id == CID, course);
                return updated.IsAcknowledged;
            }

            return false;
        }

        public async Task<bool> UpdateTopic(string CID, string MID, string TID, CreateTopicViewModel model)
        {
            var course = await GetCourse(CID);
            var module = course.Modules.SingleOrDefault(x => x.Id == MID);
            var topic = module.Topics.SingleOrDefault(x => x.Id == TID);
            var utopic = new Topic
            {
                Id = topic.Id,
                Discription = model.Discription,
                Name = model.Name,
                Position = model.Position,
                Path = topic.Path,
                Type = model.Type
            };
            var result1 = module.Topics.Remove(topic);

            if (result1)
            {
                course.Modules.Remove(module);
                module.Topics.Add(utopic);
                course.Modules.Add(module);

                var updated = await _course.ReplaceOneAsync(x => x.Id == CID, course);
                return updated.IsAcknowledged;
            }
            return false;
        }
        #endregion

        #region Delete
        public async Task<bool> DeleteStudentFromCourse(string CID, string SID)
        {
            var course = await GetCourse(CID);
            course.Doctors.Remove(course.Doctors.Find(x => x.Id == SID));
            var result = await _course.ReplaceOneAsync(x => x.Id == CID, course);
            return result.IsAcknowledged;
        }

        public async Task<bool> DeleteModule(string CID, string MID)
        {
            var course = await GetCourse(CID);
            var module = course.Modules.SingleOrDefault(x => x.Id == MID);
            var result = course.Modules.Remove(module);

            if (result)
            {
                var deleted = await _course.ReplaceOneAsync(x => x.Id == CID, course);
                return deleted.IsAcknowledged;
            }
            return false;
        }

        public async Task<bool> DeleteTopic(string CID, string MID, string TID)
        {
            var course = await GetCourse(CID);
            var module = course.Modules.SingleOrDefault(x => x.Id == MID);
            var topic = module.Topics.SingleOrDefault(x => x.Id == TID);

            var result1 = module.Topics.Remove(topic);

            if (result1)
            {
                course.Modules.Remove(module);
                course.Modules.Add(module);
                var deleted = await _course.ReplaceOneAsync(x => x.Id == CID, course);
                return deleted.IsAcknowledged;
            }
            return false;
        }

        public async Task<bool> DeleteCourse(string CID)
        {
            var deleteResult = await _course.DeleteOneAsync(x => x.Id == CID);
            return deleteResult.IsAcknowledged;
        }

        public async Task<bool> DeleteDoctorFromCourse(string CID, string DID)
        {
            var course = await GetCourse(CID);
            course.Doctors.Remove(course.Doctors.Find(x => x.Id == DID));
            var result = await _course.ReplaceOneAsync(x => x.Id == CID, course);
            return result.IsAcknowledged;
        }
        #endregion

        #region Uploads
        public async Task<bool> UploadCourseLogo(string CID, IFormFile file)
        {
            var course = await GetCourse(CID);
            string m = course.Id + course.Name;
            if (file.Length > 0)
            {
                try
                {

                    if (!Directory.Exists(_hostingEnvironment.WebRootPath + "\\" + m + "\\"))
                    {
                        Directory.CreateDirectory(_hostingEnvironment.WebRootPath + "\\" + m + "\\");
                    }
                    string guid = Guid.NewGuid().ToString();
                    using (FileStream fileStream = System.IO.File.Create(_hostingEnvironment.WebRootPath + "\\" + m + "\\" + guid + file.FileName.Replace("\\", "s").Replace(":", "s")))
                    {
                        file.CopyTo(fileStream);
                        fileStream.Flush();
                        course.LogoPath = _hostingEnvironment.WebRootPath + "\\" + m + "\\" + guid + file.FileName;
                        var result = await _course.ReplaceOneAsync(x => x.Id == CID, course);
                        return result.IsAcknowledged;
                    }
                }
                catch
                {

                    return false;
                }

            }

            return false;
        }

        public async Task<bool> UploadModuleLogo(string CID, string MID, IFormFile file)
        {

            var course = await GetCourse(CID);
            var module = course.Modules.SingleOrDefault(x => x.Id == MID);
            string m = course.Id + course.Name + "\\" + module.Id + module.Name;
            if (file.Length > 0)
            {
                try
                {

                    if (!Directory.Exists(_hostingEnvironment.WebRootPath + "\\" + m + "\\"))
                    {
                        Directory.CreateDirectory(_hostingEnvironment.WebRootPath + "\\" + m + "\\");
                    }
                    string guid = Guid.NewGuid().ToString();
                    using (FileStream fileStream = System.IO.File.Create(_hostingEnvironment.WebRootPath + "\\" + m + "\\" + guid + file.FileName.Replace("\\", "s").Replace(":", "s")))
                    {
                        file.CopyTo(fileStream);
                        fileStream.Flush();
                        course.Modules.Remove(module);
                        module.LogoPath = _hostingEnvironment.WebRootPath + "\\" + m + "\\" + guid + file.FileName;
                        course.Modules.Add(module);
                        var result = await _course.ReplaceOneAsync(x => x.Id == CID, course);
                        return result.IsAcknowledged;
                    }
                }
                catch
                {

                    return false;
                }

            }

            return false;
        }

        public async Task<bool> UploadTopic(string CID, string MID, string TID, IFormFile file)
        {
            var course = await GetCourse(CID);
            var module = course.Modules.SingleOrDefault(x => x.Id == MID);
            var topic = module.Topics.SingleOrDefault(x => x.Id == TID);
            string m = course.Id + course.Name + "\\" + module.Id + module.Name + "\\" + topic.Id + topic.Name;
            if (file.Length > 0)
            {
                try
                {

                    if (!Directory.Exists(_hostingEnvironment.WebRootPath + "\\" + m + "\\"))
                    {
                        Directory.CreateDirectory(_hostingEnvironment.WebRootPath + "\\" + m + "\\");
                    }
                    string guid = Guid.NewGuid().ToString();
                    using (FileStream fileStream = System.IO.File.Create(_hostingEnvironment.WebRootPath + "\\" + m + "\\" + guid + file.FileName.Replace("\\", "s").Replace(":", "s")))
                    {
                        file.CopyTo(fileStream);
                        fileStream.Flush();
                        course.Modules.Remove(module);
                        module.Topics.Remove(topic);
                        topic.Path = _hostingEnvironment.WebRootPath + "\\" + m + "\\" + guid + file.FileName;
                        module.Topics.Add(topic);
                        course.Modules.Add(module);
                        var result = await _course.ReplaceOneAsync(x => x.Id == CID, course);
                        return result.IsAcknowledged;
                    }
                }
                catch
                {

                    return false;
                }

            }

            return false;
        }
        #endregion









    }
}
