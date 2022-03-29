using SchoolAPI.Models.BaseModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAPI.Models.EntityModels
{
    public class StudentClass : BaseModel
    {

        [ForeignKey("Student")]
        public int StudentId { get; set; }
        public Student Student { get; set; }

        [ForeignKey("Class")]
        public int ClassId { get; set; }
        public Class Class { get; set; }
    }
}
