using Grpc.Net.Client;
using GrpcService.Protos;

namespace Grpc.Client.Repositories
{
    public static class StudentRepository
    {
        public static async Task FindStudentById(GrpcChannel channel, int id)
        {
            var client = new RemoteStudent.RemoteStudentClient(channel);

            var input = new StudentLookupModel { StudentId = id };
            var reply = await client.GetStudentInfoAsync(input);
            Console.WriteLine($"{reply.FirstName} {reply.LastName}");
        }

        public static async Task InsertStudent(GrpcChannel channel, StudentModel student)
        {
            var client = new RemoteStudent.RemoteStudentClient(channel);

            var reply = await client.InsertStudentAsync(student);
            Console.WriteLine(reply.Result);
        }

        public static async Task UpdateStudent(GrpcChannel channel, StudentModel student)
        {
            var client = new RemoteStudent.RemoteStudentClient(channel);

            var reply = await client.UpdateStudentAsync(student);
            Console.WriteLine(reply.Result);
        }

        public static async Task DeleteStudent(GrpcChannel channel, int id)
        {
            var client = new RemoteStudent.RemoteStudentClient(channel);

            var input = new StudentLookupModel { StudentId = id };
            var reply = await client.DeleteStudentAsync(input);
            Console.WriteLine(reply.Result);
        }

        public static async Task DisplayAllStudents(GrpcChannel channel)
        {
            var client = new RemoteStudent.RemoteStudentClient(channel);

            var empty = new Empty();
            var list = await client.RetrieveAllStudentsAsync(empty);

            Console.WriteLine(">>>>>>>>>>>>>>>>>>++++++++++++<<<<<<<<<<<<<<<<<<<<<<<<<<<<");

            foreach (var item in list.Items)
            {
                Console.WriteLine($"{item.StudentId}: {item.FirstName} {item.LastName}");
            }
        }
    }
}
