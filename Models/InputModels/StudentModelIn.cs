using SchoolAPI.Models.EntityModels;

namespace SchoolAPI.Models.InputModels
{
    public class StudentModelIn
    {
        public int? Id { get; set; } = null;
        public string? Name { get; set; }
        public string? Email { get; set; }
    }
}
