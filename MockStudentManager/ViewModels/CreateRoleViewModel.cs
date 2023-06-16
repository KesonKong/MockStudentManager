using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MockStudentManager.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required(ErrorMessage = "请输入角色名称")]
        [Display(Name = "角色名称")]
        public string RoleName { get; set; }
    }
}
