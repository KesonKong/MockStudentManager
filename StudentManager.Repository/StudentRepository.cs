using StudentManager.DBModels;
using StudentManager.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentManager.Repository
{
    public class StudentRepository: IStudentRepository
    {
        private IEnumerable<Student> _students;

        public StudentRepository()
        {
            _students = new List<Student>() {
            new Student () {Id=1, Name="张三", ClassName="一年级", Email = "zhangsan@126.com"},
            new Student () {Id=2, Name="李四", ClassName="二年级", Email = "lisi@126.com"},
            new Student () {Id=3, Name="王五", ClassName="三年级", Email = "wangwu@126.com"}
            };
        }

        public Student GetStudent(int id)
        {
            return _students.First(x => x.Id == id);
        }

        public IEnumerable<Student> GetAllStudents()
        {
            return _students;
        }
    }
}
