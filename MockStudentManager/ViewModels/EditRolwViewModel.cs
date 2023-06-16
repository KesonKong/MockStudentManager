using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MockStudentManager.ViewModels
{
    public class EditRolwViewModel
    {
        public EditRolwViewModel()
        {
            Users = new List<string>();
        }


        [Display(Name ="角色ID")]
        public string RoleId { get; set; }

        [Required(ErrorMessage ="角色名称不能为空")]
        [Display(Name ="角色名称")]
        public string RoleName { get; set; }


        public List<string> Users { get; set; }
    }
}
