using System.ComponentModel.DataAnnotations;

namespace MockStudentManager.ViewModels
{
    /// <summary>
    /// 用户注册信息ViewModel
    /// </summary>
    public class LoginViewModel
    {
        [Required(ErrorMessage = "请填写邮箱")]
        [Display(Name = "邮箱地址")]
        [EmailAddress(ErrorMessage = "邮箱格式不正确")]
        public string Email { get; set; }

        [Required(ErrorMessage ="请填写密码")]
        [Display(Name = "密码")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name ="记住我")]
        public bool RememberMe { get; set; }
    }
}
