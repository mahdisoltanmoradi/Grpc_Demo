// See https://aka.ms/new-console-template for more information

using Grpc.Client.Repositories;
using Grpc.Net.Client;

var channel = GrpcChannel.ForAddress("http://localhost:5007");

//-------------------ActionHello----------------------
await DefaultRepository.Hello();


//-------------------InsertStudent----------------------
//StudentModel newStudent = new StudentModel()
//{
//    FirstName = "Mahdi",
//    LastName = "Soltanmoradi",
//    School = "Tourism",
//};
//await StudentRepository.InsertStudent(channel, newStudent);

//-------------------UpdateStudent--------------------
//StudentModel updStudent = new StudentModel()
//{
//    StudentId = 56,
//    FirstName = "AAAA",
//    LastName = "ZZZ",
//    School = "Medicine",
//};
//await StudentRepository.UpdateStudent(channel, updStudent);

//-----------------DeleteStudent--------------------
//await StudentRepository.DeleteStudent(channel, 56);

//-----------------GetAllStudent--------------------
//await StudentRepository.DisplayAllStudents(channel);

Console.ReadLine();


