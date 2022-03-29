using SchoolAPI.Models.BaseModels;

namespace SchoolAPI.Models.EntityModels
{
    public class Teacher : Person
    {
        public ICollection<Class> Classes { get; set; }

    }
}
