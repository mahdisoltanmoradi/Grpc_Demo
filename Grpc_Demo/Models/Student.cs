using System.ComponentModel.DataAnnotations;

namespace Grpc_Demo.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string School { get; set; }
    }
}
