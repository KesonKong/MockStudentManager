using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentManager.DBModels
{
    public static class ModelBuilderExtensions
    {
        /// <summary>
        /// 扩展数据
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    Id = 1,
                    Name = "孔垂良",
                    ClassName = ClassNameEnum.SecondGrade,
                    Email = "kongchuiliang@126.com"
                },
                new Student
                {
                    Id = 2,
                    Name = "曹燕",
                    ClassName = ClassNameEnum.FileGrade,
                    Email = "caoyan@126.com"
                },
                new Student
                {
                    Id = 3,
                    Name = "孔佑嘉",
                    ClassName = ClassNameEnum.FileGrade,
                    Email = "kongyoujia@126.com"
                }
                );
        }

    }
}
