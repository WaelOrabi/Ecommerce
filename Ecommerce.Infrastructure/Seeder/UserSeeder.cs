using Ecommerce.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace SchoolProject.infrastructure.Seeder
{
    public static class UserSeeder
    {
        public static async Task SeedAsync(UserManager<User> _userManager)
        {
            var usersCount = await _userManager.Users.CountAsync();
            if (usersCount <= 0)
            {
                var defaulUser = new User()
                {
                    UserName = "wael_orabi",
                    Email = "wael@gmail.com",
                    FullName = "Wael Orabi",
                    PhoneNumber = "0981078432",
                    Country = "Syria",
                    Address = "Damascus",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,

                };
                await _userManager.CreateAsync(defaulUser, "wael#123W");
                await _userManager.AddToRoleAsync(defaulUser, "Admin");
            }
        }
    }
}
