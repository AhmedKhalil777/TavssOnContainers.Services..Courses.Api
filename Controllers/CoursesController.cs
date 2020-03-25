using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Course.Api.Contracts.V1.Requests;
using Course.Api.Domain;
using Course.Api.Services;
using Course.Api.V1.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Course.Api.Controllers
{
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly IMongoCourseService _MongocourseService;
        public CoursesController( IMongoCourseService mongoCourseService)
        {
            _MongocourseService = mongoCourseService;
        }

        #region Get

        [HttpGet(ApiRoutes.Student.GetDoctorsByCourseId)]
        [HttpGet(ApiRoutes.Course.GetCourseDoctors)]
        public async Task<IActionResult> GetCourseDoctors([FromRoute] string CID) {
            
            return Ok(await _MongocourseService.GetCourseDoctors(CID));
        }

        [HttpGet(ApiRoutes.Doctor.GetStudentsByCourseId)]
        [HttpGet(ApiRoutes.Course.GetCourseStudents)]
        public async Task<IActionResult> GetCourseStudents([FromRoute] string CID) {
            
            return Ok(await _MongocourseService.GetCourseStudents(CID));
        }

        [HttpGet(ApiRoutes.Course.GetCourses)]
        public async  Task<IActionResult> GetCourses()=> Ok(await _MongocourseService.GetCourses());   
        [HttpGet(ApiRoutes.Doctor.GetCourseById)]
        [HttpGet(ApiRoutes.Student.GetCourseById)]
        [HttpGet(ApiRoutes.Course.GetCourseById)]
        public async Task<IActionResult> GetCourse([FromRoute] string CID)=> Ok(await _MongocourseService.GetCourse(CID));

        [HttpGet(ApiRoutes.Student.GetModuleById)]
        [HttpGet(ApiRoutes.Doctor.GetModuleById)]
        public async Task<IActionResult> GetCoGetModuleById([FromRoute] string CID , [FromRoute] string MID) =>
            Ok(await _MongocourseService.GetModuleById(CID, MID));


        [HttpGet(ApiRoutes.Student.GetTopicById)]
        [HttpGet(ApiRoutes.Doctor.GetTopicById)]
        public async Task<IActionResult> GetTopicById([FromRoute] string CID, [FromRoute] string MID, [FromRoute] string TID) =>
            Ok(await _MongocourseService.GetTopicById(CID, MID, TID));

        #region Doctor
        [HttpGet(ApiRoutes.Doctor.GetCoursesForDoctor)]
        public async Task<IActionResult> GetCoursesForDoctor([FromRoute]string DID) => Ok(await _MongocourseService.GetCoursesForDoctor(DID));
        #endregion

        #region Student
        [HttpGet(ApiRoutes.Student.GetCoursesForStudent)]
        public async Task<IActionResult> GetCoursesForStudent([FromRoute]string SID) => Ok(await _MongocourseService.GetCoursesForStudent(SID));

        #endregion

        //[HttpGet(ApiRoutes.Doctor.GetModuleById)]
        //[HttpGet(ApiRoutes.Student.GetModuleById)]


        //[HttpGet(ApiRoutes.Doctor.GetTopicById)]
        //[HttpGet(ApiRoutes.Student.GetTopicById)]






        #endregion

        #region Create
        [HttpPost(ApiRoutes.Course.CreateCourse)]
        public async Task<IActionResult> CreateCourse([FromForm] CreateCourseViewModel course)
        {
            if (course == null)
            {
                return BadRequest("Null Name");
            }



            var result = await _MongocourseService.CreateCourse( course);
            if (result)
            {
                return Ok(new { Status = "1", Message = "Created Successfully" });
            }
            return BadRequest(new { Status = 0, Message = "Create Failed" });
        }
        [HttpPut(ApiRoutes.Doctor.CreateModule)]
        public async Task<IActionResult> CreateModule([FromRoute] string CID, [FromBody] CreateModuleViewModel model)
        {
            var result = await _MongocourseService.CreateModule(CID, model);
            if (result)
            {
                return Ok(new { Status = "1", Message = "Created Successfully" });
            }
            return BadRequest(new { Status = 0, Message = "Create Failed" });
        }

        [HttpPut(ApiRoutes.Doctor.CreateTopic)]
        public async Task<IActionResult> CreateTopic([FromRoute] string CID, [FromRoute] string MID , [FromBody] CreateTopicViewModel model)
        {
            var result = await _MongocourseService.CreateTopic(CID,MID, model);
            if (result)
            {
                return Ok(new { Status = 1, Message = "Created Successfully" });
            }
            return BadRequest(new { status = 0, Message = "Failed To Create" });
        }
        #endregion

        #region Assigning
        [HttpPut(ApiRoutes.Course.AssignDoctorToCourse)]
        public async Task<IActionResult> AssignDoctorToCourse([FromRoute]string CID ,[FromBody] Doctor doctor)
        {
          var IsSuccess =  await _MongocourseService.AssignDoctorToCourse(CID, doctor);
            if ( IsSuccess)
            {
                return Ok(new { doctor.Id, Status = 1, Message = "Successfully Assigned" });
            }
            return BadRequest(new { Status = 0, Message = "Failed To register" });

        }
        [HttpPut(ApiRoutes.Course.AssignStudentToCourse)]
        public async Task<IActionResult> AssignStudentToCourse([FromRoute]string CID, [FromBody] StudentViewModel student)
        {
            var IsSuccess = await _MongocourseService.AssignStudentToCourse(CID, new Student
            {
                Id = student.Id,
                Name = student.Name,
                Mark = (int)student.Mark

            });
            if (IsSuccess)
            {
                return Ok(new { student.Id, Status = 1, Message = "Successfully Assigned" });
            }
            return BadRequest(new { Status = 0, Message = "Failed To register" });

        }
        #endregion

        #region Update
        [HttpPut(ApiRoutes.Course.UpdateCourse)]
        public async Task<IActionResult> UpdateCourse([FromBody]UpdateCourseViewModel course)
        {
            var result = await _MongocourseService.UpdateCourse(course);
            if (result)
            {
                return Ok(new { Status = 1, Message = "Updated Successfuly" });
            }
            return BadRequest(new { Status = 0, Message = "Failed to Update" });
        }
        [HttpPut(ApiRoutes.Doctor.UpadateModule)]
        public async Task<IActionResult> UpadateModule([FromRoute] string CID, [FromRoute] string MID, [FromBody] CreateModuleViewModel model)
        {
            var result = await _MongocourseService.UpdateModule(CID, MID,model);
            if (result)
            {
                return Ok(new { status = 1, Message = "Updated Successfully" });
            }
            return BadRequest(new { status = 0, Message = "Update Failed " });

        }
        [HttpPut(ApiRoutes.Doctor.UpdateTopic)]
        public async Task<IActionResult> UpdateTopic([FromRoute] string CID, [FromRoute] string MID,[FromRoute]string TID, [FromBody] CreateTopicViewModel model)
        {
            var result = await _MongocourseService.UpdateTopic(CID, MID,TID, model);
            if (result)
            {
                return Ok(new { status = 1, Message = "Updated Successfully" });
            }
            return BadRequest(new { status = 0, Message = "Update Failed " });

        }

        // Uploads
        [HttpPut(ApiRoutes.Doctor.UploadCourseLogo)]
        public async Task<IActionResult> UploadCourseLogo( [FromRoute] string CID , IFormFile file)
        {
            var result = await _MongocourseService.UploadCourseLogo(CID, file);
            if (result)
            {
                return Ok(new { status=1 , Message= "Uploaded Successfuly"});
            }
            return BadRequest(new { status = 0, Message = "Upload Failed" });
        }
        [HttpPut(ApiRoutes.Doctor.UploadModuleLogo)]
        public async Task<IActionResult> UploadModuleLogo([FromRoute] string CID, [FromRoute] string MID , IFormFile file)
        {
            var result = await _MongocourseService.UploadModuleLogo(CID,MID ,file);
            if (result)
            {
                return Ok(new { status = 1, Message = "Uploaded Successfuly" });
            }
            return BadRequest(new { status = 0, Message = "Upload Failed" });
        }

        [HttpPut(ApiRoutes.Doctor.UploadTopic)]
        public async Task<IActionResult> UploadTopic([FromRoute] string CID, [FromRoute] string MID, [FromRoute] string TID, IFormFile file)
        {
            var result = await _MongocourseService.UploadTopic(CID, MID,TID, file);
            if (result)
            {
                return Ok(new { status = 1, Message = "Uploaded Successfuly" });
            }
            return BadRequest(new { status = 0, Message = "Upload Failed" });
        }




        #endregion

        #region Delete
        [HttpDelete(ApiRoutes.Course.DeleteCourse)]
        public async Task<IActionResult> DeleteCourse([FromRoute] string CID)
        {
            var result = await _MongocourseService.DeleteCourse(CID);
            if (result)
            {
                return Ok(new { staus = 1 , Message= "Deleted Successfuly" });
            }
            return BadRequest(new { status = 0, ErrorMessage = "Failed to Delete" });
        }

        [HttpDelete(ApiRoutes.Doctor.DeleteModule)]
        public async Task<IActionResult> DeleteModule([FromRoute] string CID , [FromRoute] string MID)
        {
            var result = await _MongocourseService.DeleteModule(CID, MID);
            if (result)
            {
                return Ok(new { status = 1 , Message = "Deleted Successfully" });
            }
            return BadRequest(new { status = 0, Message = "Delete Failed " });

        }
        [HttpDelete(ApiRoutes.Doctor.DeleteTopic)]
        public async Task<IActionResult> DeleteTopic([FromRoute] string CID, [FromRoute] string MID , [FromRoute] string TID)
        {
            var result = await _MongocourseService.DeleteTopic(CID, MID,TID);
            if (result)
            {
                return Ok(new { status = 1, Message = "Deleted Successfully" });
            }
            return BadRequest(new { status = 0, Message = "Delete Failed " });

        }

        [HttpDelete(ApiRoutes.Course.DeleteDoctorFromCourse)]
        public async Task<IActionResult> DeleteDoctorFromCourse([FromRoute] string CID,[FromRoute] string DID )
        {
            var result = await _MongocourseService.DeleteDoctorFromCourse(CID , DID);
            if (result)
            {
                return Ok(new { staus = 1, Message = "Deleted Successfuly" });
            }
            return BadRequest(new { status = 0, ErrorMessage = "Failed to Delete" });
        }

        [HttpDelete(ApiRoutes.Course.DeleteStudentFromCourse)]
        public async Task<IActionResult> DeleteStudentFromCourse([FromRoute] string CID, [FromRoute] string SID)
        {
            var result = await _MongocourseService.DeleteStudentFromCourse(CID, SID);
            if (result)
            {
                return Ok(new { staus = 1, Message = "Deleted Successfuly" });
            }
            return BadRequest(new { status = 0, ErrorMessage = "Failed to Delete" });
        }




        #endregion

    }
}