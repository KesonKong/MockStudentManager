using Microsoft.AspNetCore.Mvc;
using MockStudentManager.CustomerUtil;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MockStudentManager.ViewModels
{
    /// <summary>
    /// 用户注册信息ViewModel
    /// </summary>
    public class EditUserViewModel
    {
        public EditUserViewModel()
        {
            Roles = new List<string>();
            Claims = new List<string>();
        }

        [Required]
        [Display(Name = "用户ID")]
        public string Id { get; set; }

        [Required]
        [Display(Name = "邮箱地址")]
        [EmailAddress(ErrorMessage = "邮箱格式不正确")]
        //[Remote(action:"IsEmailInUse",controller:"Account")]
        [ValidEmailDomain(allowedDomain:"126.com", ErrorMessage ="电子邮件的后缀必须是126.com")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        //用户角色
        public List<string> Roles { get; set; }
        //用户声明
        public List<string> Claims { get; set; }

        [Display(Name = "城市")]
        public string City { get; set; }
    }
}
