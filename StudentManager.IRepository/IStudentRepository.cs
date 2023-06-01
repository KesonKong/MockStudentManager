using StudentManager.DBModels;
using System;
using System.Collections.Generic;

namespace StudentManager.IRepository
{
    public interface IStudentRepository
    {
        /// <summary>
        /// 通过Id获取一名学生信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Student GetStudent(int id);

        /// <summary>
        /// 获取全部学生信息
        /// </summary>
        /// <returns></returns>
        IEnumerable<Student> GetAllStudents();


        /// <summary>
        /// 新增一名学生信息
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        Student Add(Student student);

        /// <summary>
        /// 更新学生信息
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        Student Update(Student student);

        /// <summary>
        /// 删除学生信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Student Delete(int id);

    }
}
