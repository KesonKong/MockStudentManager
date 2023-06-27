using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MockStudentManager.ViewModels;
using StudentManager.DBModels;
using System.Threading.Tasks;

namespace MockStudentManager.Controllers
{

    [AllowAnonymous] //允许匿名访问
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;

        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    City = model.City,
                };

                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout(RegisterViewModel model)
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }


        #region 登录功能


        [HttpGet]
        public IActionResult Login()
        {
            ViewBag.ReturnUrl = HttpContext.Request.Query["ReturnUrl"].ToString();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model,string ReturnUrl)
        {
            
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(ReturnUrl))
                    {
                        if (Url.IsLocalUrl(ReturnUrl))  //防止开放性URL跳转攻击
                        {
                            return Redirect(ReturnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }

                        //return LocalRedirect(ReturnUrl); //判断是否为本地连接
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

                ModelState.AddModelError(string.Empty, "登录失败，请重试");
            }

            return View(model);
        }

        #endregion

        [AcceptVerbs("Get","Post")]
        [AllowAnonymous]
        public async Task<IActionResult> IsEmailInUse(string Email)
        {
            var user = await userManager.FindByEmailAsync(Email);
            if (user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"邮箱：{Email}已被注册使用");
            }
      
        }


        public string AccessDenied(string ReturnUrl)
        {
            return $"您没有权限访问,页面路径：{ReturnUrl}";
        }
    }
}
