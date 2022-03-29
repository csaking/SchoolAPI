using Microsoft.EntityFrameworkCore;
using SchoolAPI.Models.EntityModels;

namespace SchoolAPI.Repositories
{
    public static class ClassRepository
    {
        public static async Task<Class> CreateClassAsync(Class classroom, SchoolApiContext context)
        {
            await context.Classes.AddAsync(classroom);
            await context.SaveChangesAsync();

            return classroom;
        }

        public static async Task<IEnumerable<Class>> GetAllClassesAsync(SchoolApiContext context)
        {
            return await context.Classes.ToListAsync();
        }
        public static async Task<Class?> GetClassByIdAsync(int id, SchoolApiContext context)
        {
            return await context.Classes
                .Include(x => x.Teacher)
                .FirstOrDefaultAsync(x => x.Id == id)
                ;
        }

        public static async Task<bool> ClassExistsAsync(int id, SchoolApiContext context)
        {
            return context.Classes.Any(e => e.Id == id);
        }

        public static async Task<Class> UpdateClassByIdAsync(Class classroom, SchoolApiContext context)
        {
            context.Entry(classroom).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return classroom;
        }

        public static async Task<bool> DeleteClassByIdAsync(Class classroom, SchoolApiContext context)
        {
            context.Classes.Remove(classroom);
            await context.SaveChangesAsync();

            return true;
        }

        public static async Task<IEnumerable<Class>> GetClassesByTeacherIdAsync(int teacherId, SchoolApiContext context)
        {
            return await context.Classes.Where(x => x.TeacherId == teacherId).ToListAsync();
        }

        public static async Task<IEnumerable<Class>> GetClassesByIdArrayAsync(int[] classIds, SchoolApiContext context)
        {
            return await context.Classes.Where(x => classIds.Contains(x.Id)).ToListAsync();
        }

        public static async Task AssignClassesToTeacherAsync(Teacher teacher, SchoolApiContext context)
        {
            context.Classes.UpdateRange(teacher.Classes);
            await context.SaveChangesAsync();
        }
    }
}
