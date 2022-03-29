using SchoolAPI.Models.EntityModels;
using SchoolAPI.Repositories;

namespace SchoolAPI.Services
{
    public static class StudentClassService
    {

        public static async Task<IEnumerable<StudentClass>> GetStudentClassesByClassIdAsync(int classId, SchoolApiContext context)
        {
            return await StudentClassRepository.GetStudentClassesByClassIdAsync(classId, context);
        }

        public static async Task<IEnumerable<StudentClass>> GetStudentClassesByStudentIdAsync(int studentId, SchoolApiContext context)
        {
            return await StudentClassRepository.GetStudentClassesByStudentIdAsync(studentId, context);
        }

        public static async Task<bool> StudentClassExistsAsync(int classId, int studentId, SchoolApiContext context)
        {
            return await StudentClassRepository.StudentClassExistsAsync(classId, studentId, context);
        }

        public static async Task<StudentClass> CreateStudentClassAsync(int classId, int studentId, SchoolApiContext context)
        {
            var studentClass = new StudentClass();
            studentClass.StudentId = studentId;
            studentClass.ClassId = classId;

            return await StudentClassRepository.CreateStudentClassAsync(studentClass, context);
        }
    }
}
