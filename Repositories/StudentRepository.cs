using Microsoft.EntityFrameworkCore;
using SchoolAPI.Models.EntityModels;

namespace SchoolAPI.Repositories
{
    public static class StudentRepository
    {
        public static async Task<Student> CreateStudentAsync(Student student, SchoolApiContext context)
        {
            await context.Students.AddAsync(student);
            await context.SaveChangesAsync();

            return student;
        }

        public static async Task<IEnumerable<Student>> GetAllStudentsAsync(SchoolApiContext context)
        {
            return await context.Students.ToListAsync();
        }
        public static async Task<Student?> GetStudentByIdAsync(int id, SchoolApiContext context)
        {
            return await context.Students.FindAsync(id);
        }

        public static async Task<bool> StudentExistsAsync(int id, SchoolApiContext context)
        {
            return context.Students.Any(e => e.Id == id);
        }

        public static async Task<Student> UpdateStudentByIdAsync(Student student, SchoolApiContext context)
        {
            context.Entry(student).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return student;
        }

        public static async Task<bool> DeleteStudentByIdAsync(Student student, SchoolApiContext context)
        {
            context.Students.Remove(student);
            await context.SaveChangesAsync();

            return true;
        }

        public static async Task<IEnumerable<Student>> GetStudentsByIdArrayAsync(int[] ids, SchoolApiContext context)
        {
            return await context.Students
                .Where(x => ids.Contains(x.Id))
                .ToListAsync();
        }

    }
}
