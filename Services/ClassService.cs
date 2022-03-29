using AutoMapper;
using SchoolAPI.Models.EntityModels;
using SchoolAPI.Models.InputModels;
using SchoolAPI.Repositories;

namespace SchoolAPI.Services
{
    public static class ClassService
    {
        public static async Task<Class> CreateClassAsync(ClassModelIn model, IMapper mapper, SchoolApiContext context)
        {
            var classroom = mapper.Map<ClassModelIn, Class>(model);

            if (model.TeacherId.HasValue)
            {
                try
                {
                    var teacher = await TeacherRepository.GetTeacherByIdAsync(model.TeacherId.Value, context);
                    classroom.Teacher = teacher;
                }
                catch
                {
                    return null;
                }
            }

            await ClassRepository.CreateClassAsync(classroom, context);

            return classroom;
        }

        public static async Task<IEnumerable<Class>> GetAllClasssAsync(SchoolApiContext context)
        {
            return await ClassRepository.GetAllClassesAsync(context);
        }

        public static async Task<Class?> GetClassByIdAsync(int id, SchoolApiContext context)
        {
            return await ClassRepository.GetClassByIdAsync(id, context);
        }

        public static async Task<bool> ClassExistsAsync(int id, SchoolApiContext context)
        {
            return await ClassRepository.ClassExistsAsync(id, context);
        }

        public static async Task<Class> UpdateClassByIdAsync(ClassModelIn model, IMapper mapper, SchoolApiContext context)
        {
            var existingClass = await ClassRepository.GetClassByIdAsync(model.Id.Value, context);
            var classroom = mapper.Map<ClassModelIn, Class>(model, existingClass);

            return await ClassRepository.UpdateClassByIdAsync(classroom, context);
        }

        public static async Task<bool> DeleteClassByIdAsync(int id, SchoolApiContext context)
        {
            var classroom = await ClassRepository.GetClassByIdAsync(id, context);

            return await ClassRepository.DeleteClassByIdAsync(classroom, context);
        }

        public static async Task<IEnumerable<Class>> GetClassesByTeacherIdAsync(int teacherId, SchoolApiContext context)
        {
            return await ClassRepository.GetClassesByTeacherIdAsync(teacherId, context);
        }

        public static async Task<IEnumerable<Class>> GetClassesByIdArrayAsync(int[] classIds, SchoolApiContext context)
        {
            return await ClassRepository.GetClassesByIdArrayAsync(classIds, context);
        }

        public static async Task<IEnumerable<Class>> AssignClassesToTeacherAsync(Teacher teacher, IEnumerable<Class> classes, SchoolApiContext context)
        {
            foreach (var classroom in classes)
            {
                classroom.TeacherId = teacher.Id;
                classroom.Teacher = teacher;
                teacher.Classes.Add(classroom);
            }

            await ClassRepository.AssignClassesToTeacherAsync(teacher, context);

            return teacher.Classes;
        }

        public static async Task<Class> EnrolStudentsInClassAsync(Class classroom, IEnumerable<Student> students, SchoolApiContext context)
        {
            List<StudentClass> studentClasses = new List<StudentClass>();

            foreach(var student in students)
            {
                if(await StudentClassService.StudentClassExistsAsync(classroom.Id, student.Id, context))
                {
                    //If student already assigned to class, continue on to next Student.
                    continue;
                }
                else
                {
                    studentClasses.Add(await StudentClassService.CreateStudentClassAsync(classroom.Id, student.Id, context));
                }
            }

            classroom.StudentClasses = studentClasses;

            return await ClassRepository.UpdateClassByIdAsync(classroom, context);
        }
    }
}
