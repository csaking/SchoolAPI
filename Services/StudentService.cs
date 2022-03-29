using AutoMapper;
using SchoolAPI.Models.EntityModels;
using SchoolAPI.Models.InputModels;
using SchoolAPI.Repositories;

namespace SchoolAPI.Services
{
    public static class StudentService
    {
        public static async Task<Student> CreateStudentAsync(StudentModelIn model, IMapper mapper, SchoolApiContext context)
        {
            var student = mapper.Map<StudentModelIn, Student>(model);

            return await StudentRepository.CreateStudentAsync(student, context);
        }

        public static async Task<IEnumerable<Student>> GetAllStudentsAsync(SchoolApiContext context)
        {
            return await StudentRepository.GetAllStudentsAsync(context);
        }

        public static async Task<Student?> GetStudentByIdAsync(int id, SchoolApiContext context)
        {
            var student = await StudentRepository.GetStudentByIdAsync(id, context);

            var studentClasses = await StudentClassService.GetStudentClassesByStudentIdAsync(id, context);

            student.StudentClasses = studentClasses.ToList();

            return student;
        }

        public static async Task<bool> StudentExistsAsync(int id, SchoolApiContext context)
        {
            return await StudentRepository.StudentExistsAsync(id, context);
        }

        public static async Task<Student> UpdateStudentByIdAsync(StudentModelIn model, IMapper mapper, SchoolApiContext context)
        {
            var existingStudent = await StudentRepository.GetStudentByIdAsync(model.Id.Value, context);
            var student = mapper.Map<StudentModelIn, Student>(model, existingStudent);

            return await StudentRepository.UpdateStudentByIdAsync(student, context);
        }

        public static async Task<bool> DeleteStudentByIdAsync(int id, SchoolApiContext context)
        {
            var student = await StudentRepository.GetStudentByIdAsync(id, context);

            return await StudentRepository.DeleteStudentByIdAsync(student, context);
        }

        public static async Task<IEnumerable<Student>> GetStudentsByIdArrayAsync(int[] ids, SchoolApiContext context)
        {
            return await StudentRepository.GetStudentsByIdArrayAsync(ids, context);
        }

        public static async Task<IEnumerable<Student>> GetStudentsByClassIdAsync(int classId, SchoolApiContext context)
        {
            var studentClasses = await StudentClassService.GetStudentClassesByClassIdAsync(classId, context);

            var studentIds = studentClasses.Select(x => x.StudentId).ToArray();

            return await StudentRepository.GetStudentsByIdArrayAsync(studentIds, context);
        }
    }
}
