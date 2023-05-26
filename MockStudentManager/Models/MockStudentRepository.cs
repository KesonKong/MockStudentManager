using System.Collections.Generic;
using System.Linq;

namespace MockStudentManager.Models
{
    public class MockStudentRepository: IStudentRepository
    {
        private List<Student> _students;

        public MockStudentRepository()
        {
            _students = new List<Student>() {
                new Student(){ Id=1, Name = "张三", ClassName = "一年级",Email = "Tony-zhang@52abp.com"},
                new Student(){ Id=2, Name = "李四", ClassName = "二年级",Email = "lisi@52abp.com"},
                new Student(){ Id=3, Name = "王五", ClassName = "二年级",Email = "wangwu@52abp.com"}
            };

        }

        public IEnumerable<Student> GetAllStudents()
        {
            return _students;
        }

        public Student GetStudent(int id)
        {
            return _students.FirstOrDefault(t => t.Id == id);
        }
    }
}
