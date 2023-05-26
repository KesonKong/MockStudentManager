using System.Collections.Generic;

namespace MockStudentManager.Models
{
    public interface IStudentRepository
    {
        Student GetStudent(int id);

        IEnumerable<Student> GetAllStudents();
    }
}
