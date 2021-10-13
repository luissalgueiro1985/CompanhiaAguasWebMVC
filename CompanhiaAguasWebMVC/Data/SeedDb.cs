using CompanhiaAguasWebMVC.Data.Entities;
using CompanhiaAguasWebMVC.Helpers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace CompanhiaAguasWebMVC.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private Random _random;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _random = new Random();
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();

            await _userHelper.CheckRoleAsync("Admin");
            await _userHelper.CheckRoleAsync("Employee");
            await _userHelper.CheckRoleAsync("Customer");



            var user = await _userHelper.GetUserByEmailAsync("luisandresalgueiro@gmail.com");

            if (user == null)
            {
                user = new User
                {
                    FirstName = "Luís",
                    LastName = "Salgueiro",
                    Email = "luisandresalgueiro@gmail.com",
                    UserName = "luisandresalgueiro@gmail.com",
                    PhoneNumber = "214190000",

                };

                var result = await _userHelper.AddUserAsync(user, "123456");

                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }

                await _userHelper.AddUserToRoleAsync(user, "Admin");

            }

            var isInRole = await _userHelper.IsUserInRoleAsync(user, "Admin");

            if (!isInRole)
            {
                await _userHelper.AddUserToRoleAsync(user, "Admin");

            }


        }
    }
}
