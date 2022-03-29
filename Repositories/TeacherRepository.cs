using Microsoft.EntityFrameworkCore;
using SchoolAPI.Models.EntityModels;

namespace SchoolAPI.Repositories
{
    public static class TeacherRepository
    {
        public static async Task<Teacher> CreateTeacherAsync(Teacher teacher, SchoolApiContext context)
        {
            await context.Teachers.AddAsync(teacher);
            await context.SaveChangesAsync();

            return teacher;
        }

        public static async Task<IEnumerable<Teacher>> GetAllTeachersAsync(SchoolApiContext context)
        {
            return await context.Teachers.ToListAsync();
        }
        public static async Task<Teacher?> GetTeacherByIdAsync(int id, SchoolApiContext context)
        {
            return await context.Teachers.FindAsync(id);
        }

        public static async Task<bool> TeacherExistsAsync(int id, SchoolApiContext context)
        {
            return await context.Teachers.AnyAsync(e => e.Id == id);
        }

        public static async Task<Teacher> UpdateTeacherByIdAsync(Teacher teacher, SchoolApiContext context)
        {
            context.Entry(teacher).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return teacher;
        }

        public static async Task<bool> DeleteTeacherByIdAsync(Teacher teacher, SchoolApiContext context)
        {
            context.Teachers.Remove(teacher);
            await context.SaveChangesAsync();

            return true;
        }
    }
}
