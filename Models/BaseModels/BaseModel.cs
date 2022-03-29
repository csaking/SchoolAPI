using System.ComponentModel.DataAnnotations;

namespace SchoolAPI.Models.BaseModels
{
    public class BaseModel
    {
        [Key]
        public int Id { get; set; }
    }
}

