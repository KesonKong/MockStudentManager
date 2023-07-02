using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MockStudentManager.ViewModels;
using StudentManager.DBModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MockStudentManager.Controllers
{
    [Authorize(Roles ="管理员")]
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<AdminController> logger;

        public AdminController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, ILogger<AdminController> logger)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.logger = logger;
        }

        #region 角色管理

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {

            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = model.RoleName
                };

                var result = await roleManager.CreateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles", "Admin");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return View(model);
        }


        [HttpGet]
        public IActionResult ListRoles()
        {
            var listRoles = roleManager.Roles.ToList();
            return View(listRoles);
        }

        [Authorize(policy:"EditRolePolicy")]
        [HttpGet]
        public async Task<IActionResult> EditRole(string Id)
        {
            var role = await roleManager.FindByIdAsync(Id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"角色Id为{Id}的信息不存在";

                return View("NotFound");
            }

            var model = new EditRolwViewModel
            {
                RoleId = role.Id,
                RoleName = role.Name,
            };

            var users = userManager.Users.ToList();

            foreach(var user in users)
            {
                if (await userManager.IsInRoleAsync(user, role.Name))  //判断用户是否有该权限
                {
                    model.Users.Add(user.UserName);
                }
            }

            return View(model); 
        }


        [HttpPost]
        public async Task<IActionResult> EditRole(EditRolwViewModel Model)
        {
            var role = await roleManager.FindByIdAsync(Model.RoleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"角色Id为{Model.RoleId}的信息不存在";

                return View("NotFound");
            }
            else
            {
                role.Name = Model.RoleName;
                var result = await roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles", "Admin");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return View(Model);
        }

        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            ViewBag.roleId = roleId;

            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"角色Id为{roleId}的信息不存在";

                return View("NotFound");
            }

            var model = new List<UserRoleViewModel>();

            foreach (var user in userManager.Users)
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };

                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }

                model.Add(userRoleViewModel);

            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"角色Id为{roleId}的信息不存在";

                return View("NotFound");
            }
            else
            {
                IdentityResult result = null;

                foreach (var userRoleViewModel in model) {

                    var user = await userManager.FindByIdAsync(userRoleViewModel.UserId);
                    var isInRole = await userManager.IsInRoleAsync(user, role.Name);

                    if (userRoleViewModel.IsSelected && !isInRole)
                    {
                        //关联权限
                        result = await userManager.AddToRoleAsync(user, role.Name);
                    }
                    else if (!userRoleViewModel.IsSelected && isInRole)
                    {
                        //移除权限
                        result = await userManager.RemoveFromRoleAsync(user, role.Name);
                    }
                    else
                    {
                        continue;
                    }
                }

                if(result.Succeeded)
                {

                    return RedirectToAction("EditRole", "Admin", new { id = roleId });
                }
            }
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> DeleteRole(string Id)
        {
            var role = await roleManager.FindByIdAsync(Id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"无法找到ID为：{Id}的用户";
                return View("NotFound");
            }
            else
            {
                try
                {
                    var result = await roleManager.DeleteAsync(role);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("ListRoles", "Admin");
                    }

                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
                catch (DbUpdateException ex)
                {
                    logger.LogError($"发生异常：{ex}");
                    ViewBag.ErrorTitle = $"角色：{role.Name}正在被使用中...";
                    ViewBag.ErrorMessage = $"无法删除{role.Name}角色，因为此角色中刚已经存在用户。如果想要删除次角色，需要先删除该角色中的用户，然后再尝试删除";
                    return View("Error");
                }

            }

            return View();
        }



        #endregion

        #region 用户管理
        [HttpGet]
        public IActionResult ListUsers()
        { 
            var Users = userManager.Users.ToList();
            return View(Users);
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"无法找到ID为：{id}的用户";
                return View("NotFound");
            }

            var userClaims = await userManager.GetClaimsAsync(user);
            var userRoles = await userManager.GetRolesAsync(user);

            var model = new EditUserViewModel() {
                Id = user.Id,
                Email= user.Email,
                UserName = user.UserName,
                City = user.City,
                Claims = userClaims.Select(x => x.Value).ToList(),
                Roles = userRoles.ToList(),
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(model.Id);

                if (user == null)
                {
                    ViewBag.ErrorMessage = $"无法找到ID为：{model.Id}的用户";
                    return View("NotFound");
                }
                else
                {
                    user.Email = model.Email;
                    user.UserName = model.UserName;
                    user.City = model.City;
                    var result = await userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("ListUsers", "Admin");
                    }

                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }

                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string Id)
        {
            var user = await userManager.FindByIdAsync(Id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"无法找到ID为：{Id}的用户";
                return View("NotFound");
            }
            else
            {
                var result = await userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListUsers", "Admin");
                }

                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }

            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ManageUserRoles(string userId)
        {
            ViewBag.userId = userId;

            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"无法找到ID为：{userId}的用户";
                return View("NotFound");
            }

            var model = new List<RolesInUserViewModel>();

            foreach (var role in roleManager.Roles)
            {
                var rolesInUserViewModel = new RolesInUserViewModel() { 
                    RoleId = role.Id,
                    RoleName = role.Name,
                };

                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    rolesInUserViewModel.IsSelected = true;
                }
                else
                {
                    rolesInUserViewModel.IsSelected = false;
                }

                model.Add(rolesInUserViewModel);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ManageUserRoles(List<RolesInUserViewModel> model, string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"无法找到ID为：{userId}的用户";
                return View("NotFound");
            }

            var roles = await userManager.GetRolesAsync(user);

            //移除当前用户所有的角色信息
            var result = await userManager.RemoveFromRolesAsync(user, roles);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "无法删除用户中的角色");
                return View(model);
            }
            else
            {
                //查询出模型列表中被选中的rolename 添加到用户中
                result = await userManager.AddToRolesAsync(user, model.Where(x => x.IsSelected).Select(y => y.RoleName));

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "无法删除用户中的角色");
                    return View(model);
                }
            }

            return RedirectToAction("EditUser", "Admin", new { Id = userId });
        }


        [HttpGet]
        public async Task<IActionResult> ManageUserClaims(string userId)
        {
            ViewBag.userId = userId;

            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"无法找到ID为：{userId}的用户";
                return View("NotFound");
            }

            //获取当前用户下所有的声明
            var existingUserClaims = await userManager.GetClaimsAsync(user);

            var model = new UserClaimsViewModel { UserId = userId};
            //遍历所有的声明
            foreach (var claim in ClaimsStore.GetClaims)
            {
                UserClaim userClaim = new UserClaim()
                {
                    ClaimType = claim.Type,
                };

                //如果选中声明，则IsSelected 为true
                if (existingUserClaims.Any(c => c.Type == claim.Type))
                {
                    userClaim.IsSelected = true;
                }

                model.Claims.Add(userClaim);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ManageUserClaims(UserClaimsViewModel model) {

            var user = await userManager.FindByIdAsync(model.UserId);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"无法找到ID为：{model.UserId}的用户";
                return View("NotFound");
            }

            //查找所有的声明
            var claims = await userManager.GetClaimsAsync(user);

            //移除当前用户所有的角色信息
            var result = await userManager.RemoveClaimsAsync(user, claims);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "无法删除用户中的声明");
                return View(model);
            }
            else
            {
                //查询出模型列表中被选中的rolename 添加到用户中
                result = await userManager.AddClaimsAsync(user, model.Claims.Where(c => c.IsSelected).Select(c => new Claim(c.ClaimType, c.ClaimType)));

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "无法向用户添加选中的声明");
                    return View(model);
                }
            }

            return RedirectToAction("EditUser", "Admin", new { Id = model.UserId });
        }

        #endregion

        #region 拒绝访问

        [AllowAnonymous]
        public IActionResult AccessDenied() {
            return View();
        }

        #endregion
    }
}
