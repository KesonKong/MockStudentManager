using Microsoft.Extensions.Logging;
using StudentManager.DBModels;
using StudentManager.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentManager.Repository
{
    public class SQLStudentRepository : IStudentRepository
    {
        private readonly AppDbContext context;
        private readonly ILogger<SQLStudentRepository> logger;

        public SQLStudentRepository(AppDbContext context, ILogger<SQLStudentRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public Student Add(Student student)
        {
            context.Students.Add(student);
            context.SaveChanges();
            return student;
        }

        public Student Delete(int id)
        {
            Student student = context.Students.Find(id);
            if (student != null)
            { 
                context.Students.Remove(student);
            }
            return student;
        }

        public IEnumerable<Student> GetAllStudents()
        {
            logger.LogTrace("Trace跟踪Log");
            logger.LogDebug("Debug调试Log");
            logger.LogInformation("Information信息Log");
            logger.LogWarning("Warning警告Log");
            logger.LogError("Error错误Log");
            logger.LogCritical("Critical严重Log");

            return context.Students;
        }

        public Student GetStudent(int id)
        {
            return context.Students.Find(id);
        }

        public Student Update(Student updatestudent)
        {
            var student =  context.Students.Attach(updatestudent);
            student.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return updatestudent;
        }
    }
}
