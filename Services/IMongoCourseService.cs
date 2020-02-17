using Identity.Api.Contracts.V1.Requests;
using Identity.Api.Contracts.V1.Responses;
using Identity.Api.Domain;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Api.Services
{
    public interface IMongoCourseService
    {
        Task<List<Student>> GetCourseStudents(string CID);
        Task<bool> DeleteDoctorFromCourse(string CID, string DID);
        Task<bool> DeleteStudentFromCourse(string CID, string SID);
        Task<bool> DeleteCourse(string CID);
        Task<List<Doctor>> GetCourseDoctors(string CId);
        Task<MongoCourseDto> GetCourse(string Id);
        Task<Module> GetModuleById(string CID, string MID);
        Task<Topic> GetTopicById(string CID, string MID, string TID);
        Task<bool> UpdateCourse(UpdateCourseViewModel course);
        Task<bool> CreateCourse(CreateCourseViewModel course);
        Task<IEnumerable<MinCourseViewModel>> GetCourses();
        Task<bool> AssignDoctorToCourse(string CID, Doctor doctor);
        Task<bool> AssignStudentToCourse(string CID, Student student);
        Task<IEnumerable<MinCourseViewModel>> GetCoursesForDoctor(string DID);
        Task<IEnumerable<MinCourseViewModel>> GetCoursesForStudent(string SID);
        Task<bool> CreateModule(string CID, CreateModuleViewModel module);
        Task<bool> CreateTopic(string CID, string MID, CreateTopicViewModel model);
        Task<bool> UpdateModule(string CID, string MID, CreateModuleViewModel model);
        Task<bool> UpdateTopic(string CID, string MID, string TID, CreateTopicViewModel model);
        Task<bool> DeleteModule(string CID, string MID);
        Task<bool> DeleteTopic(string CID, string MID, string TID);
        Task<bool> UploadCourseLogo(string CID, IFormFile file);
        Task<bool> UploadModuleLogo(string CID, string MID, IFormFile file);
        Task<bool> UploadTopic(string CID, string MID, string TID, IFormFile file);

    }
}
