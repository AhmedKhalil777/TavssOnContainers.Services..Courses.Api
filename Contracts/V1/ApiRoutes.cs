using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Api.V1.Contracts
{

    public class ApiRoutes
    {
        public const string Root = "api/";

        public const string Version = "v1/";
        public const string SuperCourse = "SuperCourse";
        public const string DoctorsCourse = "DoctorCourse";
        public const string StudentsCourse = "StudentCourse";

        public const string BaseCourse = Root + Version +SuperCourse+ "/";
        public const string BaseCourseMongo = Root + Version +SuperCourse+ "/Mongo";
        public const string DoctorCourse = Root + Version + DoctorsCourse + "/";
        public const string StudentCourse = Root + Version + StudentsCourse + "/";

        public static class Course
        {

            //Read
            public const string GetCourses = BaseCourse + "GetCourses";
            public const string GetCourseById = BaseCourse + "GetCourseById/{CID}";
            public const string GetCourseStudents = BaseCourse + "GetCourseStudents/{CID}";
            public const string GetCourseDoctors = BaseCourse + "GetCourseDoctors/{CID}";

            //Create
            public const string CreateCourse = BaseCourse + "CreateCourse";
            public const string CreateCourseMongo = BaseCourse + "CreateCourseMongo";
            public const string AssignDoctorToCourse = BaseCourse + "AssignDoctorToCourse/{CID}";
            public const string AssignStudentToCourse = BaseCourse + "AssignStudentToCourse/{CID}";

            //Update
            public const string UpdateCourse = BaseCourse + "UpdateCourse";


            //Delete
            public const string DeleteCourse = BaseCourse + "DeleteCourse/{CID}";
            public const string DeleteDoctorFromCourse = BaseCourse + "DeleteDoctorFromCourse/{CID}/{DID}";
            public const string DeleteStudentFromCourse = BaseCourse + "DeleteStudentFromCourse/{CID}/{SID}";

        }
        public static class CourseMongo
        {

            //Read
            public const string GetCourses = BaseCourseMongo + "GetCourses";
            public const string GetCourseById = BaseCourseMongo + "GetCourseById/{CID}";
            public const string GetCourseStudents = BaseCourseMongo + "GetCourseStudents/{CID}";
            public const string GetCourseDoctors = BaseCourseMongo + "GetCourseDoctors/{CID}";

            //Create
            public const string CreateCourse = BaseCourseMongo + "CreateCourse";
            public const string CreateCourseMongo = BaseCourseMongo + "CreateCourseMongo";
            public const string AssignDoctorToCourse = BaseCourseMongo + "AssignDoctorToCourse/{CID}";
            public const string AssignStudentToCourse = BaseCourseMongo + "AssignStudentToCourse/{CID}";
            public const string CreateModule = BaseCourseMongo + "CreateModule/{CID}";
            public const string CreateTopic = BaseCourseMongo + "CreateTopic/{CID}/{MID}";
            //Update
            public const string UpdateCourse = BaseCourseMongo + "UpdateCourse";


            //Delete
            public const string DeleteCourse = BaseCourseMongo + "DeleteCourse/{CID}";
            public const string DeleteDoctorFromCourse = BaseCourseMongo + "DeleteDoctorFromCourse/{CID}/{DID}";
            public const string DeleteStudentFromCourse = BaseCourseMongo + "DeleteStudentFromCourse/{CID}/{SID}";

        }
        public static class Student
        {
            //Read
            public const string GetCoursesForStudent = StudentCourse + "GetCourses/{SID}";
            public const string GetCourseById = StudentCourse + "GetCourse/{CID}";
            public const string GetModuleById = StudentCourse + "GetModuleById/{CID}/{MID}";
            public const string GetTopicById = StudentCourse + "GetTopicById/{CID}/{MID}/{TID}";
            public const string GetDoctorsByCourseId = StudentCourse + "GetDoctorsByCourseId/{CID}";

        }

        public static class Doctor
        {
            //Read
            public const string GetCoursesForDoctor = DoctorCourse + "GetCourses/{DID}";
            public const string GetCourseById = DoctorCourse + "GetCourse/{CID}";
            public const string GetModuleById = DoctorCourse + "GetModuleById/{CID}/{MID}";
            public const string GetTopicById = DoctorCourse + "GetTopicById/{CID}/{MID}/{TID}";
            public const string GetStudentsByCourseId = DoctorCourse + "GetStudentsByCourseId/{CID}";

            // Create
            public const string CreateModule = DoctorCourse + "CreateModule/{CID}";
            public const string CreateTopic = DoctorCourse + "CreateTopic/{CID}/{MID}";

            // Update
            public const string UpadateModule = DoctorCourse + "UpdateModule/{CID}/{MID}";
            public const string UpdateTopic = DoctorCourse + "UpdateTopic/{CID}/{MID}/{TID}";
            public const string UploadTopic = DoctorCourse + "UploadTopic/{CID}/{MID}/{TID}";
            public const string UploadCourseLogo = DoctorCourse + "UploadCourseLogo/{CID}";
            public const string UploadModuleLogo = DoctorCourse + "UploadModuleLogo/{CID}/{MID}";

            //Delete
            public const string DeleteModule = DoctorCourse + "DeleteModule/{CID}/{MID}";
            public const string DeleteTopic = DoctorCourse + "DeleteTopic/{CID}/{MID}/{TID}";




        }



    }
}
