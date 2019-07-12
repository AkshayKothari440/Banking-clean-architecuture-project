using Manga.Application.Authentication;
using Manga.Application.Boundaries.Login;
using Manga.Application.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manga.Infrastructure.IdentityAuthentication
{
    public class LoginUser : ILoginUserService
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IGenerateToken generateToken;

        public LoginUser(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IGenerateToken generateToken)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.generateToken = generateToken;
        }

        public LoginOutput Execute(IdentityUser user, string password)
        {
            return LoginAsync(user, password).Result;
        }

        public async Task<IdentityUser> GetEmailUser(string email)
        {
            return await userManager.FindByEmailAsync(email);
        }
        public async Task<IdentityUser> GetNameUser(string name)
        {
            return await userManager.FindByNameAsync(name);
        }
        public async Task<IdentityUser> GetMobileUser(string mobile)
        {
            return userManager.Users.SingleOrDefault(m=>m.PhoneNumber==mobile);
        }

        private async Task<LoginOutput> LoginAsync(IdentityUser user, string password)
        {
            //var result = await signInManager.PasswordSignInAsync(username, password, true, lockoutOnFailure: true);
            var result = await signInManager.PasswordSignInAsync(user,password,true,false);

            if (result.Succeeded)
            {
                //var user = userManager.FindByNameAsync(username).Result;

                var token = await generateToken.GetToken(user.UserName, user);

                return new LoginOutput { CustomerId = new Guid(user.Id), Name = user.UserName, Token = token };
            }

            return null;

        }
    }
}