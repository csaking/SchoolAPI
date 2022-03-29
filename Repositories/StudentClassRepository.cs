using Microsoft.EntityFrameworkCore;
using SchoolAPI.Models.EntityModels;

namespace SchoolAPI.Repositories
{
    public static class StudentClassRepository
    {
        public static async Task<IEnumerable<StudentClass>> GetStudentClassesByClassIdAsync(int classId, SchoolApiContext context)
        {
            return await context.StudentClasses
                .Where(x => x.ClassId == classId)
                .ToListAsync();
        }

        public static async Task<IEnumerable<StudentClass>> GetStudentClassesByStudentIdAsync(int studentId, SchoolApiContext context)
        {
            return await context.StudentClasses
                .Where(x => x.StudentId == studentId)
                .ToListAsync();
        }

        public static async Task<bool> StudentClassExistsAsync(int classId, int studentId, SchoolApiContext context)
        {       
            return await context.StudentClasses.AnyAsync(e => 
                e.StudentId == studentId
                && e.ClassId == classId
                );
        }

        public static async Task<StudentClass> CreateStudentClassAsync(StudentClass studentClass, SchoolApiContext context)
        {
            await context.StudentClasses.AddAsync(studentClass);
            await context.SaveChangesAsync();

            return studentClass;
        }
    }
}
