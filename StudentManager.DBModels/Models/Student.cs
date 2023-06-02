﻿using System;
using System.ComponentModel.DataAnnotations;

namespace StudentManager.DBModels
{
    public class Student
    {
        public int Id { get; set; }

        [Display(Name = "姓名")]
        [Required(ErrorMessage = "请输入名字")]
        [MaxLength(50, ErrorMessage = "名字的长度不能超过50个字符")]
        public string Name { get; set; }

        [Display(Name = "邮箱地址")]
        [Required(ErrorMessage = "请输入邮箱地址")]
        [RegularExpression(@"^[a-zA-Z0-9_-]+@[a-zA-Z0-9_-]+(.[a-zA-Z0-9_-]+)+$",ErrorMessage = "邮箱地址格式不正确")]
        public string Email { get; set; }

        [Display(Name = "班级信息")]
        [Required(ErrorMessage = "请选择班级信息")]
        public ClassNameEnum? ClassName { get; set; }

        public string PhotoPath { get; set; }
        public int deletePropety { get; set; }
    }
}
