using AtlasBlog.Data;
using AtlasBlog.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AtlasBlog.Services
{
    public class DataService
    {
        // ------------ CALLING A METHOD OR INSTRUCTION THAT EXECUTES THE MIGRATIONS INSTRUCTIONS ----------
        readonly ApplicationDbContext _dbContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<BlogUser> _userManager;

        public DataService(ApplicationDbContext dbContext,
                            RoleManager<IdentityRole> roleManager,
                                UserManager<BlogUser> userManager)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IdentityRole RoleManager { get; }

        public async Task SetupDbAsync()
        {
            // ---------------  _RUN THE MIGRATIONS ASYNC --------------
            await _dbContext.Database.MigrateAsync();

            // Add ROLES INTO THE SYSTEM ------------------------
            await SeedRolesAsync();

            // Add USERS INTO THE SYSTEM -------------------------
            await SeedUsersAsync();
        }

        
        private async Task SeedUsersAsync()
        {
            BlogUser adminUser = new()
            {
                UserName = "joewatson@email.com",
                Email = "joewatson@email.com",
                FirstName = "joe",
                LastName = "watson",
                DisplayName = "joewatson",
                PhoneNumber = "555-555-5555",
                EmailConfirmed = true
            };

            BlogUser modUser = new()
            {
                UserName = "mrwatson@email.com",
                Email = "mrwatson@email.com",
                FirstName = "mr",
                LastName = "watson",
                DisplayName = "mrwatson",
                PhoneNumber = "555-555-5555",
                EmailConfirmed = true
            };

            try
            {
                 
                if (await _userManager.FindByEmailAsync(adminUser.Email) is null)
                {
                    await _userManager.CreateAsync(adminUser, "Abc&123!");
                    await _userManager.AddToRoleAsync(adminUser, "Administrator");
                }
                                
                                
                if (await _userManager.FindByEmailAsync(modUser.Email) is null)
                {
                    await _userManager.CreateAsync(modUser, "Abc&123!");
                    await _userManager.AddToRoleAsync(modUser, "Moderator");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        private async Task SeedRolesAsync()
        {
            if (_roleManager.Roles.Any())
            {
                return;
            }
            
            await _roleManager.CreateAsync(new IdentityRole("Administrator"));
            await _roleManager.CreateAsync(new IdentityRole("Moderator"));

        }
    }

}
