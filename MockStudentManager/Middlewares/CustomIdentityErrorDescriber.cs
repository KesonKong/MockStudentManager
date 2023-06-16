using Microsoft.AspNetCore.Identity;
using MockStudentManager.Properties;

namespace MockStudentManager.Middlewares
{
    public class CustomIdentityErrorDescriber: IdentityErrorDescriber
    {
        //
        // 摘要:
        //     Returns the default Microsoft.AspNetCore.Identity.IdentityError.
        //
        // 返回结果:
        //     The default Microsoft.AspNetCore.Identity.IdentityError.
        public override IdentityError DefaultError()
        {
            return new IdentityError
            {
                Code = nameof(DefaultError),
                Description = "发生未知故障"
            };
        }

        //
        // 摘要:
        //     Returns an Microsoft.AspNetCore.Identity.IdentityError indicating a concurrency
        //     failure.
        //
        // 返回结果:
        //     An Microsoft.AspNetCore.Identity.IdentityError indicating a concurrency failure.
        public override IdentityError ConcurrencyFailure()
        {
            return new IdentityError
            {
                Code = nameof(ConcurrencyFailure),
                Description = "并发故障"
            };
        }

        //
        // 摘要:
        //     Returns an Microsoft.AspNetCore.Identity.IdentityError indicating a password
        //     mismatch.
        //
        // 返回结果:
        //     An Microsoft.AspNetCore.Identity.IdentityError indicating a password mismatch.
        public override IdentityError PasswordMismatch()
        {
            return new IdentityError
            {
                Code = nameof(PasswordMismatch),
                Description = "密码不匹配"
            };
        }

        //
        // 摘要:
        //     Returns an Microsoft.AspNetCore.Identity.IdentityError indicating an invalid
        //     token.
        //
        // 返回结果:
        //     An Microsoft.AspNetCore.Identity.IdentityError indicating an invalid token.
        public override IdentityError InvalidToken()
        {
            return new IdentityError
            {
                Code = nameof(InvalidToken),
                Description = "无效令牌"
            };
        }

        //
        // 摘要:
        //     Returns an Microsoft.AspNetCore.Identity.IdentityError indicating a recovery
        //     code was not redeemed.
        //
        // 返回结果:
        //     An Microsoft.AspNetCore.Identity.IdentityError indicating a recovery code was
        //     not redeemed.
        public override IdentityError RecoveryCodeRedemptionFailed()
        {
            return new IdentityError
            {
                Code = nameof(RecoveryCodeRedemptionFailed),
                Description = "恢复代码兑换失败"
            };
        }

        //
        // 摘要:
        //     Returns an Microsoft.AspNetCore.Identity.IdentityError indicating an external
        //     login is already associated with an account.
        //
        // 返回结果:
        //     An Microsoft.AspNetCore.Identity.IdentityError indicating an external login is
        //     already associated with an account.
        public override IdentityError LoginAlreadyAssociated()
        {
            return new IdentityError
            {
                Code = nameof(LoginAlreadyAssociated),
                Description = "已关联登录"
            };
        }

        //
        // 摘要:
        //     Returns an Microsoft.AspNetCore.Identity.IdentityError indicating the specified
        //     user userName is invalid.
        //
        // 参数:
        //   userName:
        //     The user name that is invalid.
        //
        // 返回结果:
        //     An Microsoft.AspNetCore.Identity.IdentityError indicating the specified user
        //     userName is invalid.
        public override IdentityError InvalidUserName(string userName)
        {
            return new IdentityError
            {
                Code = nameof(InvalidUserName),
                Description = "无效用户名"
            };
        }

        //
        // 摘要:
        //     Returns an Microsoft.AspNetCore.Identity.IdentityError indicating the specified
        //     email is invalid.
        //
        // 参数:
        //   email:
        //     The email that is invalid.
        //
        // 返回结果:
        //     An Microsoft.AspNetCore.Identity.IdentityError indicating the specified email
        //     is invalid.
        public override IdentityError InvalidEmail(string email)
        {
            return new IdentityError
            {
                Code = nameof(InvalidEmail),
                Description = "无效电子邮件"
            };
        }

        //
        // 摘要:
        //     Returns an Microsoft.AspNetCore.Identity.IdentityError indicating the specified
        //     userName already exists.
        //
        // 参数:
        //   userName:
        //     The user name that already exists.
        //
        // 返回结果:
        //     An Microsoft.AspNetCore.Identity.IdentityError indicating the specified userName
        //     already exists.
        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError
            {
                Code = nameof(DuplicateUserName),
                Description = "重复用户名"
            };
        }

        //
        // 摘要:
        //     Returns an Microsoft.AspNetCore.Identity.IdentityError indicating the specified
        //     email is already associated with an account.
        //
        // 参数:
        //   email:
        //     The email that is already associated with an account.
        //
        // 返回结果:
        //     An Microsoft.AspNetCore.Identity.IdentityError indicating the specified email
        //     is already associated with an account.
        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError
            {
                Code = nameof(DuplicateEmail),
                Description = "重复电子邮件"
            };
        }

        //
        // 摘要:
        //     Returns an Microsoft.AspNetCore.Identity.IdentityError indicating the specified
        //     role name is invalid.
        //
        // 参数:
        //   role:
        //     The invalid role.
        //
        // 返回结果:
        //     An Microsoft.AspNetCore.Identity.IdentityError indicating the specific role role
        //     name is invalid.
        public override IdentityError InvalidRoleName(string role)
        {
            return new IdentityError
            {
                Code = nameof(InvalidRoleName),
                Description = "无效角色名称"
            };
        }

        //
        // 摘要:
        //     Returns an Microsoft.AspNetCore.Identity.IdentityError indicating the specified
        //     role name already exists.
        //
        // 参数:
        //   role:
        //     The duplicate role.
        //
        // 返回结果:
        //     An Microsoft.AspNetCore.Identity.IdentityError indicating the specific role role
        //     name already exists.
        public override IdentityError DuplicateRoleName(string role)
        {
            return new IdentityError
            {
                Code = nameof(DuplicateRoleName),
                Description = "重复文件名"
            };
        }

        //
        // 摘要:
        //     Returns an Microsoft.AspNetCore.Identity.IdentityError indicating a user already
        //     has a password.
        //
        // 返回结果:
        //     An Microsoft.AspNetCore.Identity.IdentityError indicating a user already has
        //     a password.
        public override IdentityError UserAlreadyHasPassword()
        {
            return new IdentityError
            {
                Code = nameof(UserAlreadyHasPassword),
                Description = "用户已就绪且有密码"
            };
        }

        //
        // 摘要:
        //     Returns an Microsoft.AspNetCore.Identity.IdentityError indicating user lockout
        //     is not enabled.
        //
        // 返回结果:
        //     An Microsoft.AspNetCore.Identity.IdentityError indicating user lockout is not
        //     enabled.
        public override IdentityError UserLockoutNotEnabled()
        {
            return new IdentityError
            {
                Code = nameof(UserLockoutNotEnabled),
                Description = "未启用用户锁定"
            };
        }

        //
        // 摘要:
        //     Returns an Microsoft.AspNetCore.Identity.IdentityError indicating a user is already
        //     in the specified role.
        //
        // 参数:
        //   role:
        //     The duplicate role.
        //
        // 返回结果:
        //     An Microsoft.AspNetCore.Identity.IdentityError indicating a user is already in
        //     the specified role.
        public override IdentityError UserAlreadyInRole(string role)
        {
            return new IdentityError
            {
                Code = nameof(UserAlreadyInRole),
                Description = "用户已有角色"
            };
        }

        //
        // 摘要:
        //     Returns an Microsoft.AspNetCore.Identity.IdentityError indicating a user is not
        //     in the specified role.
        //
        // 参数:
        //   role:
        //     The duplicate role.
        //
        // 返回结果:
        //     An Microsoft.AspNetCore.Identity.IdentityError indicating a user is not in the
        //     specified role.
        public override IdentityError UserNotInRole(string role)
        {
            return new IdentityError
            {
                Code = nameof(UserAlreadyInRole),
                Description = "用户不在角色中"
            };
        }

        //
        // 摘要:
        //     Returns an Microsoft.AspNetCore.Identity.IdentityError indicating a password
        //     of the specified length does not meet the minimum length requirements.
        //
        // 参数:
        //   length:
        //     The length that is not long enough.
        //
        // 返回结果:
        //     An Microsoft.AspNetCore.Identity.IdentityError indicating a password of the specified
        //     length does not meet the minimum length requirements.
        public override IdentityError PasswordTooShort(int length)
        {
            return new IdentityError
            {
                Code = nameof(PasswordTooShort),
                Description = "密码过短"
            };
        }

        //
        // 摘要:
        //     Returns an Microsoft.AspNetCore.Identity.IdentityError indicating a password
        //     does not meet the minimum number uniqueChars of unique chars.
        //
        // 参数:
        //   uniqueChars:
        //     The number of different chars that must be used.
        //
        // 返回结果:
        //     An Microsoft.AspNetCore.Identity.IdentityError indicating a password does not
        //     meet the minimum number uniqueChars of unique chars.
        public override IdentityError PasswordRequiresUniqueChars(int uniqueChars)
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresUniqueChars),
                Description = "密码要求唯一字符"
            };
        }

        //
        // 摘要:
        //     Returns an Microsoft.AspNetCore.Identity.IdentityError indicating a password
        //     entered does not contain a non-alphanumeric character, which is required by the
        //     password policy.
        //
        // 返回结果:
        //     An Microsoft.AspNetCore.Identity.IdentityError indicating a password entered
        //     does not contain a non-alphanumeric character.
        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresNonAlphanumeric),
                Description = "密码要求非字母数字"
            };
        }

        //
        // 摘要:
        //     Returns an Microsoft.AspNetCore.Identity.IdentityError indicating a password
        //     entered does not contain a numeric character, which is required by the password
        //     policy.
        //
        // 返回结果:
        //     An Microsoft.AspNetCore.Identity.IdentityError indicating a password entered
        //     does not contain a numeric character.
        public override IdentityError PasswordRequiresDigit()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresDigit),
                Description = "密码要求数字"
            };
        }

        //
        // 摘要:
        //     Returns an Microsoft.AspNetCore.Identity.IdentityError indicating a password
        //     entered does not contain a lower case letter, which is required by the password
        //     policy.
        //
        // 返回结果:
        //     An Microsoft.AspNetCore.Identity.IdentityError indicating a password entered
        //     does not contain a lower case letter.
        public override IdentityError PasswordRequiresLower()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresLower),
                Description = "密码必须至少有一个小写字母（'a'-'z'）"
            };
        }

        //
        // 摘要:
        //     Returns an Microsoft.AspNetCore.Identity.IdentityError indicating a password
        //     entered does not contain an upper case letter, which is required by the password
        //     policy.
        //
        // 返回结果:
        //     An Microsoft.AspNetCore.Identity.IdentityError indicating a password entered
        //     does not contain an upper case letter.
        public override IdentityError PasswordRequiresUpper()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresUpper),
                Description = "密码必须至少有一个小写字母（'A'-'Z'）"
            };
        }
    }
}
