using StudentManager.DBModels;
using System;
using System.Collections.Generic;

namespace StudentManager.IRepository
{
    public interface IStudentRepository
    {
        Student GetStudent(int id);

        IEnumerable<Student> GetAllStudents();
    }
}
