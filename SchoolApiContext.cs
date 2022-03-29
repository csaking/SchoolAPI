using Microsoft.EntityFrameworkCore;
using SchoolAPI.Models.EntityModels;

namespace SchoolAPI
{
    public class SchoolApiContext : DbContext
    {
        public SchoolApiContext(DbContextOptions<SchoolApiContext> options) : base(options){}

        public DbSet<Class> Classes { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }

        public DbSet<StudentClass> StudentClasses { get; set; }
    }
}
