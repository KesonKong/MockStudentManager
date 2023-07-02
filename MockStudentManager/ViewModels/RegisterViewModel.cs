using Microsoft.AspNetCore.Mvc;
using MockStudentManager.CustomerUtil;
using System.ComponentModel.DataAnnotations;

namespace MockStudentManager.ViewModels
{
    /// <summary>
    /// 用户注册信息ViewModel
    /// </summary>
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="邮箱不能为空")]
        [Display(Name = "邮箱地址")]
        [EmailAddress(ErrorMessage = "邮箱格式不正确")]
        [Remote(action:"IsEmailInUse",controller:"Account")]
        [ValidEmailDomain(allowedDomain:"126.com", ErrorMessage ="电子邮件的后缀必须是126.com")]
        public string Email { get; set; }

        [Required(ErrorMessage = "密码不能为空")]
        [Display(Name = "密码")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "确认密码不能为空")]
        [DataType(DataType.Password)]
        [Display(Name ="确认密码")]
        [Compare("Password",ErrorMessage = "两次输入的密码不一致")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "城市")]
        public string City { get; set; }
    }
}
