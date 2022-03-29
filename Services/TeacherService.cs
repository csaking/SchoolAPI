using AutoMapper;
using SchoolAPI.Models.EntityModels;
using SchoolAPI.Models.InputModels;
using SchoolAPI.Repositories;

namespace SchoolAPI.Services
{
    public static class TeacherService
    {
        public static async Task<Teacher> CreateTeacherAsync(TeacherModelIn model, IMapper mapper, SchoolApiContext context)
        {
            var teacher = mapper.Map<TeacherModelIn, Teacher>(model);

            return await TeacherRepository.CreateTeacherAsync(teacher, context);
        }

        public static async Task<IEnumerable<Teacher>> GetAllTeachersAsync(SchoolApiContext context)
        {
            return await TeacherRepository.GetAllTeachersAsync(context);
        }

        public static async Task<Teacher?> GetTeacherByIdAsync(int id, SchoolApiContext context)
        {
            var teacher = await TeacherRepository.GetTeacherByIdAsync(id, context);

            var teacherClasses = await ClassService.GetClassesByTeacherIdAsync(id, context);

            teacher.Classes = teacherClasses.ToList();

            return teacher;
        }

        public static async Task<bool> TeacherExistsAsync(int id, SchoolApiContext context)
        {
            return await TeacherRepository.TeacherExistsAsync(id, context);
        }

        public static async Task<Teacher> UpdateTeacherByIdAsync(TeacherModelIn model, IMapper mapper, SchoolApiContext context)
        {
            var existingTeacher = await TeacherRepository.GetTeacherByIdAsync(model.Id.Value, context);
            var teacher = mapper.Map<TeacherModelIn, Teacher>(model, existingTeacher);

            return await TeacherRepository.UpdateTeacherByIdAsync(teacher, context);
        }

        public static async Task<bool> DeleteTeacherByIdAsync(int id, SchoolApiContext context)
        {
            var teacher = await TeacherRepository.GetTeacherByIdAsync(id, context);

            return await TeacherRepository.DeleteTeacherByIdAsync(teacher, context);
        }
    }
}
