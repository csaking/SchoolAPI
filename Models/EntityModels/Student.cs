using SchoolAPI.Models.BaseModels;

namespace SchoolAPI.Models.EntityModels
{
    public class Student : Person
    {
        public ICollection<StudentClass> StudentClasses { get; set; }
    }
}
