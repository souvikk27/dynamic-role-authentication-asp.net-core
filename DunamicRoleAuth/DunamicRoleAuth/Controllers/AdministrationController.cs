using DunamicRoleAuth.Data;
using DunamicRoleAuth.Models;
using DunamicRoleAuth.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DunamicRoleAuth.Controllers
{
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        

        public AdministrationController(RoleManager<IdentityRole> roleManager)
        {
           this.roleManager = roleManager;  
        }

        
        public IActionResult Index()
        {
            //var roles = roleManager.Roles.Where(a => a.Name != "SuperAdmin").ToList();
            var roles = roleManager.Roles.ToList();
            return View(roles);
        }
        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(string roleName)
        {
            if (roleName != null)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName.Trim()));
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string roleName)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                return NotFound();
            }

            var result = await roleManager.DeleteAsync(role);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return RedirectToAction("Index");
        }

    }
}
