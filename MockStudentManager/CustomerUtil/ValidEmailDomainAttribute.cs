using System.ComponentModel.DataAnnotations;

namespace MockStudentManager.CustomerUtil
{
    /// <summary>
    /// 自定义验证属性，验证邮箱后缀必须一致
    /// </summary>
    public class ValidEmailDomainAttribute: ValidationAttribute
    {
        private readonly string allowedDomain;

        public ValidEmailDomainAttribute(string allowedDomain)
        {
            this.allowedDomain = allowedDomain;
        }

        public override bool IsValid(object value)
        {
            if (value == null) { return false; }

            string[] strings = value.ToString().Split('@');
            return strings[1].ToUpper() == allowedDomain.ToUpper();
        }
    }
}
