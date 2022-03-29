using SchoolAPI.Models.BaseModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAPI.Models.EntityModels
{
    public class Class : BaseModel
    {
        [Required, StringLength(20), MinLength(1)]
        public string Name { get; set; }

        [Required, Range(5, 30, ErrorMessage = "Capacity must be at least at least 1 and no greater than 5.")]
        public int Capacity { get; set; }

        //Business Rule Validation: A Class can only be assigned 1 teacher.
        [ForeignKey("Teacher")]
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public ICollection<StudentClass> StudentClasses { get; set; }
    }
}
