using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite.Internal.UrlActions;
using MockStudentManager.ViewModels;
using StudentManager.DBModels.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace MockStudentManager.Controllers
{
    [Authorize(Roles ="管理员")]
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public AdminController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

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

        [HttpGet]
        public async Task<IActionResult> EditRole(string Id)
        {
            var role = await roleManager.FindByIdAsync(Id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"角色Id为{Id}的信息不存在";

                return View("../Error/NotFound");
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

                return View("../Error/NotFound");
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

                return View("../Error/NotFound");
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

                return View("../Error/NotFound");
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

    }
}
