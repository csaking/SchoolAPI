using System.ComponentModel.DataAnnotations;

namespace SchoolAPI.Models.BaseModels
{
    public class Person : BaseModel
    {
        [Required, StringLength(50), MinLength(1)]
        public string Name { get; set; }

        public string? Email { get; set; }
    }
}
