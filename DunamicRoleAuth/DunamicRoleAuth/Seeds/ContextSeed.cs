using DunamicRoleAuth.Data;
using DunamicRoleAuth.Models;
using EnumsNET;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace DunamicRoleAuth.Seeds
{
    public static class ContextSeed
    {
        public static async Task SeedRolesAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole(UserManagement.MVC.Enums.Roles.SuperAdmin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(UserManagement.MVC.Enums.Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(UserManagement.MVC.Enums.Roles.Moderator.ToString()));
            await roleManager.CreateAsync(new IdentityRole(UserManagement.MVC.Enums.Roles.Basic.ToString()));
        }
        public static async Task SeedSuperAdminAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new ApplicationUser
            {
                UserName = "superadmin",
                Email = "souvik.vip@gmail.com",
                FirstName = "Souvik",
                LastName = "Kundu",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "Sou@2345");
                    await userManager.AddToRoleAsync(defaultUser, UserManagement.MVC.Enums.Roles.Basic.ToString());
                    await userManager.AddToRoleAsync(defaultUser, UserManagement.MVC.Enums.Roles.Moderator.ToString());
                    await userManager.AddToRoleAsync(defaultUser, UserManagement.MVC.Enums.Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(defaultUser, UserManagement.MVC.Enums.Roles.SuperAdmin.ToString());
                }
                await roleManager.SeedClaimsForSuperAdmin();
            }
        }
        private async static Task SeedClaimsForSuperAdmin(this RoleManager<IdentityRole> roleManager)
        {
            var adminRole = await roleManager.FindByNameAsync("SuperAdmin");
            await roleManager.AddPermissionClaim(adminRole, "Products");
        }
        public static async Task AddPermissionClaim(this RoleManager<IdentityRole> roleManager, IdentityRole role, string module)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            var allPermissions = Permissions.GeneratePermissionsForModule(module);
            foreach (var permission in allPermissions)
            {
                if (!allClaims.Any(a => a.Type == "Permission" && a.Value == permission))
                {
                    await roleManager.AddClaimAsync(role, new Claim("Permission", permission));
                }
            }
        }
    }
}
