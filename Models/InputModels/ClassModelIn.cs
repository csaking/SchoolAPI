namespace SchoolAPI.Models.InputModels
{
    public class ClassModelIn
    {
        public int? Id { get; set; } = null;
        public string? Name { get; set; }
        public int? Capacity { get; set; } = null;
        public int? TeacherId { get; set; } = null;
    }
}
