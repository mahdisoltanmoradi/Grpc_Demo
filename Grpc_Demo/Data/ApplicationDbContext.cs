using Grpc_Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace Grpc_Demo.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Student>().HasData(
              GetStudents()
            );
        }

        private static List<Student> GetStudents()
        {
            List<Student> students = new List<Student>() {
              new Student() {    // 1
                StudentId=11,
                FirstName="Ann",
                LastName="Fox",
                School="Nursing",
              },
              new Student() {    // 2
                StudentId=22,
                FirstName="Sam",
                LastName="Doe",
                School="Mining",
              },
              new Student() {    // 3
                StudentId=33,
                FirstName="Sue",
                LastName="Cox",
                School="Business",
              },
              new Student() {    // 4
                StudentId=44,
                FirstName="Tim",
                LastName="Lee",
                School="Computing",
              },
              new Student() {    // 5
                StudentId=55,
                FirstName="Jan",
                LastName="Ray",
                School="Computing",
              },
            };

            return students;
        }
    }
}
