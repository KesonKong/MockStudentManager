using StudentManager.DBModels;
using StudentManager.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentManager.Repository
{
    public class StudentRepository: IStudentRepository
    {
        private List<Student> _studentsList;

        public StudentRepository()
        {
            _studentsList = new List<Student>() {
            new Student () {Id=1, Name="张三", ClassName=ClassNameEnum.FileGrade, Email = "zhangsan@126.com" },
            new Student () {Id=2, Name="李四", ClassName=ClassNameEnum.SecondGrade, Email = "lisi@126.com"},
            new Student () {Id=3, Name="王五", ClassName=ClassNameEnum.ThirdGrade, Email = "wangwu@126.com"}
            };
        }

        public Student GetStudent(int id)
        {
            return _studentsList.First(x => x.Id == id);
        }

        public IEnumerable<Student> GetAllStudents()
        {
            return _studentsList;
        }

        public Student Add(Student student)
        {
            student.Id = _studentsList.Max(x => x.Id) + 1;
            _studentsList.Add(student);
            return student;
        }

        public Student Update(Student updatestudent)
        {
            Student student = _studentsList.FirstOrDefault(s => s.Id == updatestudent.Id);
            if (student != null)
            {
                student.Name = updatestudent.Name;
                student.Email = updatestudent.Name;
                student.Name = updatestudent.Name;
            }
            return student;
        }

        public Student Delete(int id)
        {
            Student student = _studentsList.FirstOrDefault(s => s.Id == id);
            if (student != null)
            {
                _studentsList.Remove(student);
            }

            return student;
        }
    }
}
