using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc_Demo.DataAccess;
using Grpc_Demo.Models;
using GrpcService.Protos;

namespace Grpc_Demo.Services
{
    public class StudentsService : RemoteStudent.RemoteStudentBase
    {
        private readonly ILogger<StudentsService> _logger;
        private readonly ApplicationDbContext _context;

        public StudentsService(ILogger<StudentsService> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }


        public override Task<StudentModel> GetStudentInfo(StudentLookupModel request, ServerCallContext context)
        {
            StudentModel output = new StudentModel();

            var student = _context.Students.Find(request.StudentId);

            _logger.LogInformation("Sending Student response");

            if (student != null)
            {
                output.StudentId = student.StudentId;
                output.FirstName = student.FirstName;
                output.LastName = student.LastName;
                output.School = student.School;
            }

            return Task.FromResult(output);
        }
        public override Task<Reply> InsertStudent(StudentModel request, ServerCallContext context)
        {
            var s = _context.Students.Find(request.StudentId);

            if (s != null)
            {
                return Task.FromResult(
                  new Reply()
                  {
                      Result = $"Student {request.FirstName} {request.LastName} already exists.",
                      IsOk = false
                  }
                );
            }

            Student student = new Student()
            {
                StudentId = request.StudentId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                School = request.School,
            };

            _logger.LogInformation("Insert student");

            try
            {
                _context.Students.Add(student);
                var returnVal = _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
            }

            return Task.FromResult(
               new Reply()
               {
                   Result = $"Student {request.FirstName} {request.LastName}  was successfully inserted.",
                   IsOk = true
               }
            );
        }

        public override Task<Reply> UpdateStudent(StudentModel request, ServerCallContext context)
        {
            var s = _context.Students.Find(request.StudentId);

            if (s == null)
            {
                return Task.FromResult(
                  new Reply()
                  {
                      Result = $"Student {request.FirstName} {request.LastName} cannot be found.",
                      IsOk = false
                  }
                );
            }

            s.FirstName = request.FirstName;
            s.LastName = request.LastName;
            s.School = request.School;

            _logger.LogInformation("Update student");

            try
            {
                var returnVal = _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
            }

            return Task.FromResult(
               new Reply()
               {
                   Result = $"Student {request.FirstName} {request.LastName} was successfully updated.",
                   IsOk = true
               }
            );
        }

        public override Task<Reply> DeleteStudent(StudentLookupModel request, ServerCallContext context)
        {
            var s = _context.Students.Find(request.StudentId);

            if (s == null)
            {
                return Task.FromResult(
                  new Reply()
                  {
                      Result = $"Student with ID {request.StudentId} cannot be found.",
                      IsOk = false
                  }
                );
            }

            _logger.LogInformation("Delete Student");

            try
            {
                _context.Students.Remove(s);
                var returnVal = _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
            }

            return Task.FromResult(
               new Reply()
               {
                   Result = $"Student with ID {request.StudentId} was successfully deleted.",
                   IsOk = true
               }
            );
        }

        public override Task<StudentList> RetrieveAllStudents(GrpcService.Protos.Empty request, ServerCallContext context)
        {
            _logger.LogInformation("Retrieving all students");

            StudentList list = new StudentList();

            try
            {
                List<StudentModel> studentList = new List<StudentModel>();

                var students = _context.Students.ToList();

                foreach (var c in students)
                {
                    studentList.Add(new StudentModel()
                    {
                        StudentId = c.StudentId,
                        FirstName = c.FirstName,
                        LastName = c.LastName,
                        School = c.School,
                    });
                }

                list.Items.AddRange(studentList);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
            }

            return Task.FromResult(list);
        }
    //public class StudentService : EmployeeCRUD.EmployeeCRUDBase
    //{
    //    private DataAccess.ApplicationDbContext db = null;
    //    public StudentService(DataAccess.ApplicationDbContext db)
    //    {
    //        this.db = db;
    //    }

    //    public override Task<Employees> SelectAll(Empty requestData, ServerCallContext context)
    //    {
    //        Employees responseData = new Employees();
    //        var query = from emp in db.Employees
    //                    select (new DataAccess.Employee
    //                    {
    //                        EmployeeID = emp.EmployeeID,
    //                        FirstName = emp.FirstName,
    //                        LastName = emp.LastName
    //                    });
                        
    //        responseData.Items.AddRange(query.ToList());
    //        return Task.FromResult(responseData);
    //    }

    //    public override Task<Employees> SelectByID(EmployeeFilter requestData, ServerCallContext context)
    //    {
    //        var data = db.Employees.Find(requestData.EmployeeID);

    //        DataAccess.Employee emp = new DataAccess.Employee()
    //        {
    //            EmployeeID = data.EmployeeID,
    //            FirstName = data.FirstName,
    //            LastName = data.LastName
    //        };
    //        return Task.FromResult(emp);
    //    }

    //    public override Task<Empty> Insert(Employee requestData, ServerCallContext context)
    //    {
    //        db.Employees.Add(new DataAccess.Employee()
    //        {
    //            EmployeeID = requestData.EmployeeID,
    //            FirstName = requestData.FirstName,
    //            LastName = requestData.LastName
    //        });
    //        db.SaveChanges();
    //        return Task.FromResult(new Empty());
    //    }


    //    public override Task<Empty> Update(Employee requestData, ServerCallContext context)
    //    {
    //        db.Employees.Update(new DataAccess.Employee()
    //        {
    //            EmployeeID = requestData.EmployeeID,
    //            FirstName = requestData.FirstName,
    //            LastName = requestData.LastName
    //        });
    //        db.SaveChanges();
    //        return Task.FromResult<object>(Empty));
    //    }

    //    public override async Task Delete(EmployeeFilter requestData, ServerCallContext context)
    //    {
    //        var data = db.Employees.Find(requestData.EmployeeID);
    //        db.Employees.Remove(new DataAccess.Employee()
    //        {
    //            EmployeeID = data.EmployeeID,
    //            FirstName = data.FirstName,
    //            LastName = data.LastName
    //        });
    //        db.SaveChanges();
    //        return await Task.FromResult<DataAccess.Employee>(null);
    //    }

    //    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    //    {
    //        if (env.IsDevelopment())
    //        {
    //            app.UseDeveloperExceptionPage();
    //        }
    //        app.UseRouting();
    //        app.UseEndpoints(endpoints =>
    //        {
    //            endpoints.MapGrpcService<StudentService>();
    //            ...
    //});
    }
}